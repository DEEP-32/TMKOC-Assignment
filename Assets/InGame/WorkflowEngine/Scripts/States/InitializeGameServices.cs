using System;
using UnityEngine;
using WorkflowEngine.Runtime.Core;

namespace WorkflowEngine.Runtime.States {
    public class InitializeGameServices : IWorkFlowState {
        public event Action OnSuccess;
        public event Action<string> OnFailure;
        
        //services to be initialized
        readonly WorkflowContext context;
        
        public InitializeGameServices(WorkflowContext context) {
            this.context = context;
        }
        
        public void OnEnter() {
            Debug.Log($"[Workflow] Entering {nameof(InitializeGameServices)}...");
            
        }
        public void OnExit() {
            Debug.Log($"[Workflow] Exiting {nameof(InitializeGameServices)}...");
        }
        public void Execute() {
            //mocking the initialization process
            OnSuccess?.Invoke();
        }

       
    }
}