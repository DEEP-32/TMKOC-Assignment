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
        INotificationLogger logger;

        Dictionary<string, object> metadata = null;

        public INotificationDelivery Delivery => delivery;

        public INotificationValidator Validator => validator;

        public INotificationFormatted Formatter => formatter;

        public INotificationLogger Logger => logger;

        public EmailNotificationPipeline(
            INotificationDelivery delivery,
            INotificationValidator validator,
            INotificationFormatted formatter,
            INotificationLogger logger) {
            this.delivery = delivery;
            this.validator = validator;
            this.formatter = formatter;
            this.logger = logger;
        }

        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            bool success = false;
            string statusMessage = "";

            try {
                if (!await validator.Validate(request)) {
                    statusMessage = "Validation failed (Missing/Invalid Email Address)";
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                success = true;
                statusMessage = "Email delivered successfully";
                return true;
            }
            catch (Exception ex) {
                statusMessage = $"SMTP Exception: {ex.Message}";
                return false;
            }
            finally {
                if (logger != null) {
                    await logger.Log(request, success, statusMessage);
                }
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