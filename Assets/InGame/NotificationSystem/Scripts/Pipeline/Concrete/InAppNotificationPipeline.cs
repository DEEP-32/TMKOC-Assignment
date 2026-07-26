using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Pipeline.Concrete {
    public class InAppNotificationPipeline : INotificationPipeline {
        public const string DisplayDurationKey = "DisplayDuration";
        public const string UILayerPriorityKey = "UILayerPriority";
        
        Dictionary<string, object> metadata = null;
        
        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;
        
        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator => validator;
        public INotificationFormatted Formatter => formatter;
        
        public InAppNotificationPipeline(
            INotificationDelivery delivery, 
            INotificationValidator validator, 
            INotificationFormatted formatter) 
        {
            this.delivery = delivery;
            this.validator = validator;
            this.formatter = formatter;
        }
        
        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            try {
                if (!await validator.Validate(request)) {
                    Debug.LogError("[InAppPipeline] Invalid UI payload.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                
                // For In-App, the 'Send' method likely just instantiates a prefab or triggers an event
                await delivery.Send(formattedMessage, request); 
                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"[InAppPipeline] UI rendering failed: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, object> CreatePipelineMetadata() {
            if(metadata != null) return metadata;
            
            metadata = new Dictionary<string, object> {
                { DisplayDurationKey, 3.5f }, // 3.5 seconds
                { UILayerPriorityKey, "Overlay" }
            };
            
            return metadata;
        }
    }
}