using System;
using UnityEngine;

namespace WorkflowEngine.Runtime.States {
    public class ReadyState : IWorkFlowState {
        public event Action OnSuccess;
        public event Action<string> OnFailure;
        public void OnEnter() {
            Debug.Log($"[Workflow] Entering {nameof(ReadyState)} State...");

        }
        public void OnExit() {
            Debug.Log($"[Workflow] Exiting {nameof(ReadyState)} State...");
        }
        
        public void Execute() {
            //mocking the initialization process
            OnSuccess?.Invoke();    
        }

       
    }
}