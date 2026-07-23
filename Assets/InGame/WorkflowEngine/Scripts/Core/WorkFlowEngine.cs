using System.Collections.Generic;

namespace WorkflowEngine.Runtime.Core {
    public class WorkFlowEngine : IWorkFlowEngine {
        
        List<WorkFlowStateRuntimeData> states;
        public IReadOnlyList<WorkFlowStateRuntimeData> States => states;
        
        public WorkFlowEngine(List<WorkFlowStateRuntimeData> states) {
            this.states = states;
        }
        
        
    }
}