using System;
using System.Collections.Generic;
using UnityEngine;
using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime.Core {
    public class WorkFlowEngine : IWorkFlowEngine {

        public event Action<IWorkFlowState> onStateChanged;
        public event Action onCompleted;
        public event Action<string> onWorkflowFailure;
        public IReadOnlyList<WorkFlowStateRuntimeData> States => states;

        public IWorkFlowState CurrentState {
            get {

                if (currentStateIndex >= 0 && currentStateIndex < states.Count) {
                    return states[currentStateIndex].State;
                }

                return null;
            }
                
        }
        public WorkFlowStateRuntimeData CurrentStateRuntimeData => states[currentStateIndex];
        
        List<WorkFlowStateRuntimeData> states;
        
        int currentStateIndex;
        IWorkFlowState currentState;
        int currentRetryCount;
        
        
        public WorkFlowEngine(List<WorkFlowStateRuntimeData> states) {
            this.states = states;
            currentStateIndex = 0;
            currentState = null;
            currentRetryCount = 0;
        }


        public void StartWorkflow() {
            EnterNewState();
        }

        public void FreeResources() {
            if (CurrentState != null) {
                CurrentState.OnSuccess -= HandleStateSuccess;
                CurrentState.OnFailure -= HandleStateFailure;
        
                // Let the state clean up its own internal resources (e.g., stop web requests)
                CurrentState?.OnExit(); 
            }

            // 2. Nuke all external listeners attached to this engine (What you already did!)
            onWorkflowFailure = null;
            onCompleted = null;
            onStateChanged = null;
        }

        void EnterState(IWorkFlowState currentState) {
            currentState.OnEnter();
            onStateChanged?.Invoke(currentState);
            currentRetryCount = 0;
            ExecuteState();
        }

        void EnterNewState() {
            EnterState(CurrentState);
        }


        void ExecuteState() {
            CurrentState.OnSuccess += HandleStateSuccess;
            CurrentState.OnFailure += HandleStateFailure;
            
            CurrentState.Execute();
        }




        void HandleStateSuccess() {
            CurrentState.OnSuccess -= HandleStateSuccess;
            CurrentState.OnFailure -= HandleStateFailure;
            
            CurrentState.OnExit();
            
            currentStateIndex++;
            if (currentStateIndex < states.Count) {
                EnterState(CurrentState);
            } else {
                Debug.Log($"[{nameof(WorkFlowEngine)}] all states completed!");
                onCompleted?.Invoke();
            }
        }
        
        void HandleStateFailure(string errorMsg) {
            
            Debug.Log($"[{nameof(WorkFlowEngine)}] state failed: {CurrentState.GetType().Name} with error: {errorMsg} tries left: {states[currentStateIndex].Tries - currentRetryCount}");
            
            CurrentState.OnSuccess -= HandleStateSuccess;
            CurrentState.OnFailure -= HandleStateFailure;
            
            CurrentState.OnExit();

            int maxTries = states[currentStateIndex].Tries;

            if (currentRetryCount < maxTries) {
                currentRetryCount++;
                ExecuteState();
            }

            else {
                onWorkflowFailure?.Invoke($"Halted at {CurrentState.GetType().Name} after {currentRetryCount} retries. Error: {errorMsg}");
            }
            
            
        }
        
        
        
        
        
        
        
        
    }
}