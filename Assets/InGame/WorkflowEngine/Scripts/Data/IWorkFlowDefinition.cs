using System.Collections.Generic;
using WorkFlowEngine.Runtime.Data;

namespace WorkflowEngine.Runtime.Data {
    public interface IWorkFlowDefinition {
        IReadOnlyList<WorkflowStepConfig> States { get; }
    }
}