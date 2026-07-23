using System.Collections.Generic;

namespace WorkflowEngine.Runtime.Data {
    public interface IWorkFlowDefinition {
        IReadOnlyList<string> States { get; }
    }
}