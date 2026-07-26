using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Pipeline.Concrete {
    public class SlackNotificationPipeline : INotificationPipeline {
        public const string SlackChannelKey = "slack";
        Dictionary<string, object> metadata = null;
        
        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;
        
        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator => validator;
        public INotificationFormatted Formatter => formatter;
        
        public SlackNotificationPipeline(
            INotificationDelivery delivery, 
            INotificationValidator validator, 
            INotificationFormatted formatter) 
        {
            this.delivery = delivery;
            this.validator = validator;
            this.formatter = formatter;
        }
        
        // --- COMPLETED METHOD ---
        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            try {
                bool isValid = await validator.Validate(request);
                if (!isValid) {
                    Debug.LogError("[SlackPipeline] Validation failed. Aborting delivery.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"[SlackPipeline] Critical pipeline failure: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, object> CreatePipelineMetadata() {
            if(metadata != null) return metadata;
            
            metadata = new Dictionary<string, object> {
                { SlackChannelKey, "general" }
            };
            
            return metadata;
        }
    }
}