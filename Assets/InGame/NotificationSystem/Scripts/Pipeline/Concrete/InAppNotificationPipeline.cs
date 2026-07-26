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
        INotificationLogger logger;

        public INotificationDelivery Delivery => delivery;

        public INotificationValidator Validator => validator;

        public INotificationFormatted Formatter => formatter;

        public INotificationLogger Logger => logger;

        public InAppNotificationPipeline(
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
                    statusMessage = "Validation failed (Empty UI Message Payload)";
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                success = true;
                statusMessage = "In-App UI Banner instantiated successfully";
                return true;
            }
            catch (Exception ex) {
                statusMessage = $"UI Rendering Exception: {ex.Message}";
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
                { DisplayDurationKey, 3.5f }, // 3.5 seconds
                { UILayerPriorityKey, "Overlay" }
            };

            return metadata;
        }
    }
}