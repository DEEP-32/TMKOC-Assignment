using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NotificationSystem.Runtime.Core {
    public class EmailNotificationPipeline : INotificationPipeline {
        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;
        
        Dictionary<string, object> metadata = null;
        
        public EmailNotificationPipeline(INotificationDelivery delivery, INotificationValidator validator, INotificationFormatted formatter) {
            this.delivery = delivery;
            this.validator = validator;
            this.formatter = formatter;
        }


        public Dictionary<string, object> CreatePipelineMetadata() {

            if (metadata != null) {
                return metadata;
            }
            
            metadata = new Dictionary<string, object> {
                {"pipelineId", "email"},
            };
            
            return metadata;
        }

        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator => validator;
        public INotificationFormatted Formatter => formatter;


        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            try {
                bool isValid = await validator.Validate(request);
                if (!isValid) {
                    Debug.LogError("[EmailPipeline] Validation failed. Aborting delivery.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                return true;
            }
            catch (Exception ex) {
                // Catch network timeouts, missing metadata keys, or formatting errors
                Debug.LogError($"[EmailPipeline] Critical pipeline failure: {ex.Message}");
                return false;
            }
        }
    }
}