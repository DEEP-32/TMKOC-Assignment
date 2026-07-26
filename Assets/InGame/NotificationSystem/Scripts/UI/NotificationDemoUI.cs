using System;
using System.Collections.Generic;
using NotificationSystem.Runtime.Pipeline.Config;
using UnityEngine;

namespace NotificationSystem.Runtime.UI {
    public class NotificationDemoUI : MonoBehaviour {
        [SerializeField] PipelineTrigger tiggerPrefab;
        [SerializeField] Transform container;
        
        List<PipelineTrigger> pipelineTriggers;

        public Action<string> TriggerPipeline;
        

        public void Init(IReadOnlyList<PipelineConfigEntry> config) {
            pipelineTriggers = new List<PipelineTrigger>(config.Count);
            for (var i = 0; i < config.Count; i++) {
                var pipelineConfigEntry = config[i];
                var trigger = Instantiate(tiggerPrefab, container);
                trigger.Init(pipelineConfigEntry.Type,pipelineConfigEntry.Name,i,OnPipelineTrigger);
                
                pipelineTriggers.Add(trigger);
            }

            
        }
        
        void OnPipelineTrigger(int index) {
            var pipelineId = pipelineTriggers[index].PipelineId;
            TriggerPipeline?.Invoke(pipelineId);
            
        }
        
        void OnDestroy() {
            foreach (var trigger in pipelineTriggers) {
                trigger.Button.onClick.RemoveAllListeners();
            }
            
            pipelineTriggers.Clear();
        }
    }
}