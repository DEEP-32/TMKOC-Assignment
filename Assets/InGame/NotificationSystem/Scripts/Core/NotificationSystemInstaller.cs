using NotificationSystem.Runtime.Controller;
using UnityEngine;
using NotificationSystem.Runtime.Factory;
using NotificationSystem.Runtime.Pipeline.Config;
using NotificationSystem.Runtime.Pipeline.Logging;
using NotificationSystem.Runtime.UI;
using TMKOC.Utils;

namespace NotificationSystem.Runtime.Core {
    
    public class NotificationSystemInstaller : MonoBehaviour {
        
        [Header("Model (Data)")]
        [InlineEditor, SerializeField] private NotificationPipelineConfig pipelineConfig;
        
        [Header("View (UI)")]
        [SerializeField] private NotificationDemoUI demoUI;
        
        [SerializeField] JsonHistoryLogger historyLogger;
        
        private NotificationController controller;

        void Awake() {
            var factory = new NotificationPipelineFactory(pipelineConfig.Pipelines);
            
            controller = FindFirstObjectByType<NotificationController>();

            // 2. Auto-Create it if it is missing
            if (controller == null) {
                Debug.Log("[Installer] NotificationController not found. Auto-creating...");
                
                GameObject controllerObject = new GameObject("NotificationController");
                controller = controllerObject.AddComponent<NotificationController>();
                
                controllerObject.transform.SetParent(transform);
            }

            controller.Init(pipelineConfig, demoUI, factory,historyLogger);
        }
    }
}