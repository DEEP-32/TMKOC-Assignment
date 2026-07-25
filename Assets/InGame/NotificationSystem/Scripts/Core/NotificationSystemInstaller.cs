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
            //factory.CreatePipeline("EmailPipeline");
        }

        void Init() {
            factory = new NotificationFactory(pipelineConfig.Pipelines);
        }
    }
}