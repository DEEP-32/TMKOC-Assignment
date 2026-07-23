using System;
using System.Collections.Generic;
using WorkflowEngine.Runtime.Core;
using WorkflowEngine.Runtime.Services;
using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime.Factory {
    public class WorkflowStateFactory : IWorkFlowStateFactory {
        readonly Dictionary<string, Func<IWorkFlowState>> stateCreators;


        // Constructor Injection: The Factory holds all the "ingredients" needed to build states
        public WorkflowStateFactory(
            IAuthService authService,
            IConfigService configService,
            WorkflowContext sharedContext) {
            stateCreators = new Dictionary<string, Func<IWorkFlowState>> {
                {nameof(AuthenticateState), () => new AuthenticateState(authService, sharedContext) },
                {nameof(DownloadConfigState), () => new DownloadConfigState(configService, sharedContext) },
                {nameof(ValidateConfigState), () => new ValidateConfigState((IDownloadContext)sharedContext,(IValidateContext)sharedContext) },
                { nameof(InitializeGameServices), () => new InitializeGameServices(sharedContext) },
                // { nameof(ReadyState), () => new ReadyState() }
            };
        }


        public IWorkFlowState Create(string className) {
            if (stateCreators.TryGetValue(className, out var creatorFunc)) {
                // Executes the Func, successfully injecting the dependencies!
                return creatorFunc.Invoke();
            }

            throw new ArgumentException($"Factory doesn't know how to create state: {className}");
        }
    }
}