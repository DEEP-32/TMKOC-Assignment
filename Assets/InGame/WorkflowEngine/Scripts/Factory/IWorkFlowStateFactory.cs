using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime.Factory {
    public interface IWorkFlowStateFactory {
        IWorkFlowState Create(string className);
    }
}