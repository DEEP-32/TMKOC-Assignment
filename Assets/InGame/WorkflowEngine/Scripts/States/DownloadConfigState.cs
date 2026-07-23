using System;
using UnityEngine;
using WorkflowEngine.Runtime.Core;
using WorkflowEngine.Runtime.Services;

namespace WorkflowEngine.Runtime.States {
    public class DownloadConfigState : IWorkFlowState {
        public event Action OnSuccess;

        public event Action<string> OnFailure;

        private readonly IConfigService configService;
        private readonly IDownloadContext downloadContext;

        // Constructor Injection
        public DownloadConfigState(IConfigService configService, IDownloadContext downloadContext) {
            this.configService = configService;
            this.downloadContext = downloadContext;
        }

        public void OnEnter() {
            Debug.Log("[Workflow] Entering DownloadConfigState...");
        }

        public async void Execute() {
            try {
                string token = downloadContext.SessionToken;

                if (string.IsNullOrEmpty(token)) {
                    OnFailure?.Invoke("Download Failed: Missing Session Token. Did Auth fail?");
                    return;
                }

                var result = await configService.DownloadMainConfigAsync(token);

                if (result.IsSuccessful) {
                    downloadContext.DownloadedConfigPath = result.DownloadedFilePath;
                    OnSuccess?.Invoke();
                }
                else {
                    OnFailure?.Invoke($"Download Failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex) {
                OnFailure?.Invoke($"Download Exception: {ex.Message}");
            }
        }

        public void OnExit() {
            Debug.Log("[Workflow] Exiting DownloadConfigState.");
        }
    }
}