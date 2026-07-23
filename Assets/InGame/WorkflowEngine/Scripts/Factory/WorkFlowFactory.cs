using System;
using System.Collections.Generic;
using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime.Factory {
    public class WorkFlowFactory : IWorkFlowStateFactory {
        readonly Dictionary<string, Func<IWorkFlowState>> stateCreators;
        
        public IWorkFlowState Create(string className) {
            if (stateCreators.TryGetValue(className, out var creatorFunc)) {
                // Executes the Func, successfully injecting the dependencies!
                return creatorFunc.Invoke(); 
            }

            throw new ArgumentException($"Factory doesn't know how to create state: {className}");
        }
    }
}