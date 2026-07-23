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
        
        public IWorkFlowState CurrentState => states[currentStateIndex].State;
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
            currentStateIndex++;
            if (currentStateIndex < states.Count) {
                EnterState(CurrentState);
            } else {
                Debug.Log($"[{nameof(WorkFlowEngine)}] all states completed!");
                onCompleted?.Invoke();
            }
        }
        
        void HandleStateFailure(string errorMsg) {
            CurrentState.OnSuccess -= HandleStateSuccess;
            CurrentState.OnFailure -= HandleStateFailure;

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