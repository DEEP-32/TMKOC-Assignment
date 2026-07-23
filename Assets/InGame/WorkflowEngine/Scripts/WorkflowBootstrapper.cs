using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WorkflowEngine.Runtime.Core;
using WorkFlowEngine.Runtime.Data;
using WorkflowEngine.Runtime.Factory;
using WorkflowEngine.Runtime.Services;
using WorkflowEngine.Runtime.Services.MockService;
using WorkflowEngine.Runtime.States;

namespace WorkflowEngine.Runtime {
    public class WorkflowBootstrapper : MonoBehaviour {
        [SerializeField] WorkFlowDefinition workflowDefinitionAsset;
        [SerializeField] Button startButton;
        IWorkFlowEngine workFlowEngine;

        void Awake() {
            if (workflowDefinitionAsset == null) {
                Debug.LogError("No workflow definition asset found");
                return;
            }

            WorkflowContext context = new WorkflowContext {
                LoginEmail = "random12@user.com",
                LoginPassword = "12345678"
            };

            IAuthService authService = new MockAuthService();
            IConfigService configService = new MockDownloadService();
            
            IWorkFlowStateFactory statFactory = new WorkflowStateFactory(authService, configService, context);
            
            WorkFlowStateRuntimeData[] runtimeStates = new WorkFlowStateRuntimeData[workflowDefinitionAsset.States.Count];

            try {
                for (int i = 0; i < workflowDefinitionAsset.States.Count; i++) {
                    var workflowStepConfig = workflowDefinitionAsset.States[i];
        
                    IWorkFlowState state = statFactory.Create(workflowStepConfig.stateClassName);
                    int maxTries = workflowStepConfig.maxRetries;

                    // 2. Assign directly to the index
                    runtimeStates[i] = new WorkFlowStateRuntimeData {
                        State = state,
                        Tries = maxTries
                    };
                }
            }
            catch (Exception e) {
                Debug.LogError($"[WorkflowBootstrapper] Critical failure during sequence building! Aborting workflow setup.\nDetails: {e.Message}");
                return;
            }

            workFlowEngine = new Core.WorkFlowEngine(runtimeStates.ToList());
            
            startButton.onClick.AddListener(StartWorkflow);
        }

        void StartWorkflow() {
            workFlowEngine.StartWorkflow();
        }

        void OnDestroy() {
            workFlowEngine = null;
            
        }
    }
}