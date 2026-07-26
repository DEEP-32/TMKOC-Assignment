using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Pipeline.Concrete {
    public class EmailNotificationPipeline : INotificationPipeline {
        public const string EmailKey = "email";

        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;

        Dictionary<string, object> metadata = null;

        public INotificationDelivery Delivery => delivery;

        public INotificationValidator Validator => validator;

        public INotificationFormatted Formatter => formatter;

        public EmailNotificationPipeline(
            INotificationDelivery delivery,
            INotificationValidator validator,
            INotificationFormatted formatter) {
            this.delivery = delivery;
            this.validator = validator;
            this.formatter = formatter;
        }

        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            try {
                bool isValid = await validator.Validate(request);
                if (!isValid) {
                    Debug.LogError("[EmailPipeline] Invalid email address or missing payload. Aborting delivery.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                Debug.Log("[EmailPipeline] Pipeline completed successfully.");
                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"[EmailPipeline] Critical pipeline failure: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, object> CreatePipelineMetadata() {
            if (metadata != null) {
                return metadata;
            }

            metadata = new Dictionary<string, object> {
                { EmailKey, "mock@123.com" }
            };

            return metadata;
        }
    }
}