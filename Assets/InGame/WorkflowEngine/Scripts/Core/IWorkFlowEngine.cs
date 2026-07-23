using System.Collections.Generic;
using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime.Core {

    public struct WorkFlowStateRuntimeData {
        public IWorkFlowState State;
        public int Tries;
    }
    
    public interface IWorkFlowEngine {
        IReadOnlyList<WorkFlowStateRuntimeData> States { get; }
    }
}