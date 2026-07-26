using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NotificationSystem.Runtime.Core {
    public class EmailNotificationPipeline : INotificationPipeline {
        
        public const string EmailKey = "email";
        
        public event Action<INotificationPipeline> onStarted;
        public event Action<INotificationPipeline, bool> onCompleted;
        
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
                {EmailKey, "mock@123.com"},
            };
            
            return metadata;
        }

        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator => validator;
        public INotificationFormatted Formatter => formatter;

        public void RemoveBinding() {
            onStarted = null;
            onCompleted = null;
        }


        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            try {
                bool isValid = await validator.Validate(request);
                if (!isValid) {
                    Debug.LogError("[EmailPipeline] Validation failed. Aborting delivery.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);
                
                Debug.Log("[EmailPipeline] Pipeline completed successfully");
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