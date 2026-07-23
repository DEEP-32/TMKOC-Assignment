using System;
using System.Collections.Generic;
using TMKOC.Utils;
using UnityEngine;
using WorkflowEngine.Runtime.Data;
using WorkflowEngine.Runtime.States;

namespace WorkFlowEngine.Runtime.Data {
    
    [Serializable]
    public class WorkflowStepConfig 
    {
        [TypeDropdown(typeof(IWorkFlowState))]
        public string stateClassName;
        
        [Range(0, 5)]
        public int maxRetries = 0; 
    }
    
    [CreateAssetMenu(fileName = "NewWorkflow", menuName = "Workflow/Definition", order = 0)]
    public class WorkFlowDefinition : ScriptableObject,IWorkFlowDefinition {
        [SerializeField] List<WorkflowStepConfig> workflowStates;
        public IReadOnlyList<WorkflowStepConfig> States => workflowStates;
    }
}