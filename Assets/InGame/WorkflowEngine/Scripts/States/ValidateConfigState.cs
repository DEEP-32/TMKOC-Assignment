using System;
using System.IO;
using UnityEngine;
using WorkflowEngine.Runtime.Core;
using WorkflowEngine.Runtime.Data;

namespace WorkflowEngine.Runtime.States {
    public class ValidateConfigState : IWorkFlowState {
        public event Action OnSuccess;

        public event Action<string> OnFailure;

        readonly IDownloadContext downloadContext;
        readonly IValidateContext validateContext;

        public ValidateConfigState(IDownloadContext downloadContext,IValidateContext validateContext) {
            this.downloadContext = downloadContext;
            this.validateContext = validateContext;
        }

        public void OnEnter() {
            Debug.Log("[Workflow] Entering ValidateConfigState...");
        }

        public void Execute() {
            try {
                //This is the real validation , for sake of assignment we are mocking it down below
                /*string dataPath = downloadContext.DownloadedConfigPath;

                if (string.IsNullOrEmpty(dataPath) || !File.Exists(dataPath)) {
                    OnFailure?.Invoke($"Validation Failed: Config file not found at path '{dataPath}'.");
                    return;
                }

                string jsonContent = File.ReadAllText(dataPath);
                GameData parsedData = JsonUtility.FromJson<GameData>(jsonContent);

                if (parsedData != null && !string.IsNullOrEmpty(parsedData.version)) {
                    validateContext.GameData = parsedData;
                    Debug.Log($"[Workflow] [state : {nameof(ValidateConfigState)}] successful : game data : {parsedData}");
                    OnSuccess?.Invoke();
                }
                else {
                    OnFailure?.Invoke("Validation Failed: JSON was malformed or empty.");
                }*/
                
                OnSuccess?.Invoke();
            }
            catch (Exception ex) {
                OnFailure?.Invoke($"Validation Exception: {ex.Message}");
            }
        }

        public void OnExit() {
            Debug.Log("[Workflow] Exiting ValidateConfigState.");
        }
    }
}