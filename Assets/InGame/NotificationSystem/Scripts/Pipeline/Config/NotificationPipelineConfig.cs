using System.Collections.Generic;
using NotificationSystem.Runtime.Core;
using TMKOC.Utils;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Config {

    //config for single pipeline
    [System.Serializable]
    public class PipelineConfigEntry {
        [TypeDropdown(typeof(INotificationPipeline))]
        public string Type;
        
        [field : SerializeField] public  BaseNotificationValidator Validator { get; private set; }
        [field : SerializeField] public  BaseNotificationDelivery Delivery { get; private set; }
        [field : SerializeField] public  BaseNotificationFormatter Formatter { get; private set; }
        
    }
    
    [CreateAssetMenu(fileName = "PipelineConfig", menuName = "NotificationSystem/Config", order = 0)]
    public class NotificationPipelineConfig : ScriptableObject {
        [SerializeField] List<PipelineConfigEntry> pipelines;
        
        public IReadOnlyList<PipelineConfigEntry> Pipelines => pipelines;
    }
}