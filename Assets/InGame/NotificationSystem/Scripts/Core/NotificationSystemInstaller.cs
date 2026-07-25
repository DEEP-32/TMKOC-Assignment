using System;
using NotificationSystem.Runtime.Factory;
using NotificationSystem.Runtime.Pipeline.Config;
using TMKOC.Utils;
using UnityEngine;

namespace NotificationSystem.Runtime.Core {
    public class NotificationSystemInstaller : MonoBehaviour {
        [InlineEditor,SerializeField] NotificationPipelineConfig pipelineConfig;
        INotificationFactory factory;


        void Awake() {
            Init();
        }

        void Init() {
            factory = new NotificationFactory(pipelineConfig.Pipelines);
        }
    }
}