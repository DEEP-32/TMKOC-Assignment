namespace WorkflowEngine.Runtime.States {
    public interface IWorkFlowState {
        void OnEnter();
        void OnExit();
        void Execute();
        
        event System.Action OnSuccess;
        event System.Action<string> OnFailure;
    }
}