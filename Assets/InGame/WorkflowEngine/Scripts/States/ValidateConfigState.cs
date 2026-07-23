using System;

namespace WorkflowEngine.Runtime.States {
    public class ValidateConfigState : IWorkFlowState{
        public void OnEnter() {
            
        }
        public void OnExit() {
            
        }

        public void Execute() {
            
        }

        public event Action OnSuccess;

        public event Action<string> OnFailure;
    }
}