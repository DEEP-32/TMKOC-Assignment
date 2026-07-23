using System;
using System.Collections.Generic;
using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime.Core {

    public struct WorkFlowStateRuntimeData {
        public IWorkFlowState State;
        public int Tries;
    }
    
    public interface IWorkFlowEngine {
        
        event Action<IWorkFlowState> onStateChanged;
        event Action onCompleted;
        event Action<string> onWorkflowFailure;
        IReadOnlyList<WorkFlowStateRuntimeData> States { get; }
        
        IWorkFlowState CurrentState { get; }
        
        void StartWorkflow();
        
        void FreeResources();
    }
}