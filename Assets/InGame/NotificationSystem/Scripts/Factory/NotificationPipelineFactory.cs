using System;
using System.Collections.Generic;
using UnityEngine;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Config;

namespace NotificationSystem.Runtime.Factory {
    public class NotificationPipelineFactory : INotificationPipelineFactory {
        readonly Dictionary<string, Func<INotificationPipeline>> pipelineCreators;
        
        public NotificationPipelineFactory(IReadOnlyList<PipelineConfigEntry> pipelineConfig) {
            pipelineCreators = new Dictionary<string, Func<INotificationPipeline>>();

            foreach (var pipelineConfigEntry in pipelineConfig) {
                var typeString = pipelineConfigEntry.Type; 
                
                Type targetType = Type.GetType(typeString);

                if (targetType != null) {
                    pipelineCreators[typeString] = () => Activator.CreateInstance(
                            targetType,
                            pipelineConfigEntry.Delivery,
                            pipelineConfigEntry.Validator,
                            pipelineConfigEntry.Formatter,
                            pipelineConfigEntry.Logger
                        ) as INotificationPipeline;
                }
                else {
                    Debug.LogError($"[NotificationFactory] Could not find Type for: {typeString}");
                }
            }
        }
        
        public INotificationPipeline CreatePipeline(string typeString) {
            if (pipelineCreators.TryGetValue(typeString, out var creatorFunc)) {
                return creatorFunc.Invoke(); // This triggers Activator.CreateInstance
            }
            
            Debug.LogError($"[NotificationFactory] No pipeline registered for type: {typeString}");
            return null;
        }

    }
}