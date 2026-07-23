namespace WorkFlowEngine.Runtime {
    public interface IWorkFlowState {
        void OnEnter();
        void OnExit();
        void Execute();
        
        event System.Action OnSuccess;
        event System.Action<string> OnFailure;
    }

    public interface IWorkFlowEngine {
        
    }
    
    public interface IWorkFlowStateFactory {
        IWorkFlowState Create(string className);
    }
}