using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Pipeline.Concrete {
    public class PushNotificationPipeline : INotificationPipeline {
        public const string DeviceTokenKey = "DeviceToken";
        public const string BadgeCountKey = "BadgeCount";
        public const string PlaySoundKey = "PlaySound";

        Dictionary<string, object> metadata = null;

        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;
        INotificationLogger logger;

        public INotificationDelivery Delivery => delivery;

        public INotificationValidator Validator => validator;

        public INotificationFormatted Formatter => formatter;

        public INotificationLogger Logger => logger;

        public PushNotificationPipeline(
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
                    statusMessage = "Validation failed (Missing OS Device Token)";
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                success = true;
                statusMessage = "Push Notification handed off to Native OS API";
                return true;
            }
            catch (Exception ex) {
                statusMessage = $"Native OS Push Exception: {ex.Message}";
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
                { BadgeCountKey, 1 },
                { PlaySoundKey, true },
                { DeviceTokenKey, "mock_token" }
            };

            return metadata;
        }
    }
}