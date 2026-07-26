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
        INotificationFactory factory;


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

        void TriggerPipeline(string pipelineId) {
            var newPipeline = factory.CreatePipeline(pipelineId);
            Debug.Log($"[Notification] Triggering pipeline {pipelineId} is new pipeline null : {newPipeline == null}");
        }

        void Init() {
            factory = new NotificationFactory(pipelineConfig.Pipelines);
        }
    }
}