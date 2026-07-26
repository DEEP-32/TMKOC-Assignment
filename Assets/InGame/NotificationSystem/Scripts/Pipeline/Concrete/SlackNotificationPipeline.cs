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
        INotificationLogger logger;

        public INotificationDelivery Delivery => delivery;

        public INotificationValidator Validator => validator;

        public INotificationFormatted Formatter => formatter;

        public INotificationLogger Logger => logger;

        public SlackNotificationPipeline(
            INotificationDelivery delivery,
            INotificationValidator validator,
            INotificationFormatted formatter,
            INotificationLogger logger) {
            this.delivery = delivery;
            this.validator = validator;
            this.formatter = formatter;
            this.logger = logger;
        }

        // --- COMPLETED METHOD ---
        public async Task<bool> StartNotificationPipeline(NotificationRequest request) {
            bool success = false;
            string statusMessage = "";

            try {
                if (!await validator.Validate(request)) {
                    statusMessage = "Validation failed (Missing Target Channel)";
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                success = true;
                statusMessage = "Delivered to Slack successfully";
                return true;
            }
            catch (Exception ex) {
                statusMessage = $"Slack API Exception: {ex.Message}";
                return false;
            }
            finally {
                if (logger != null) {
                    await logger.Log(request, success, statusMessage);
                }
            }
        }

        public Dictionary<string, object> CreatePipelineMetadata() {
            if (metadata != null) return metadata;

            metadata = new Dictionary<string, object> {
                { SlackChannelKey, "general" }
            };

            return metadata;
        }
    }
}