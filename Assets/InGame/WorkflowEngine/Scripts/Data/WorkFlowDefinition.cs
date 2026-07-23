using System.Collections.Generic;
using UnityEngine;
using WorkflowEngine.Runtime.Data;

namespace WorkFlowEngine.Runtime {
    [CreateAssetMenu(fileName = "NewWorkflow", menuName = "Workflow/Definition", order = 0)]
    public class WorkFlowDefinition : ScriptableObject,IWorkFlowDefinition {
        [SerializeField] List<string> workflowStates;
        public IReadOnlyList<string> States => workflowStates;
    }
}