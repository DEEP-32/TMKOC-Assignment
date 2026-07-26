using System;
using NotificationSystem.Runtime.Factory;
using NotificationSystem.Runtime.Pipeline.Config;
using NotificationSystem.Runtime.UI;
using TMKOC.Utils;
using UnityEngine;

namespace NotificationSystem.Runtime.Core {
    public class NotificationSystemInstaller : MonoBehaviour {
        [InlineEditor,SerializeField] NotificationPipelineConfig pipelineConfig;
        [SerializeField] NotificationDemoUI demoUI;
        INotificationPipelineFactory pipelinePipelineFactory;
        
        static string DummyMessage = "Hello World";


        void Awake() {
            Init();
            demoUI.Init(pipelineConfig.Pipelines);
        }

        void Start() {
            demoUI.TriggerPipeline += TriggerPipeline;
        }

        void OnDestroy() {
            demoUI.TriggerPipeline -= TriggerPipeline;
        }

        void TriggerPipeline(string pipelineType) {
            var newPipeline = pipelinePipelineFactory.CreatePipeline(pipelineType);
            var metaData = newPipeline.CreatePipelineMetadata();

            var notificationRequest = new NotificationRequest(pipelineType, DummyMessage, null, metaData);
            
            var res = newPipeline.StartNotificationPipeline(notificationRequest);

            //newPipeline.StartNotificationPipeline();
            //Debug.Log($"[Notification] Triggering pipeline {pipelineId} is new pipeline null : {newPipeline == null}");
        }

        void Init() {
            pipelinePipelineFactory = new NotificationPipelineFactory(pipelineConfig.Pipelines);
        }
    }
}