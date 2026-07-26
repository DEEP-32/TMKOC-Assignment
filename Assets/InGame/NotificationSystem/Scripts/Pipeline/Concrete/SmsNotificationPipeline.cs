using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Pipeline.Concrete {
    public class SmsNotificationPipeline : INotificationPipeline {
        public const string PhoneNumberKey = "PhoneNumber";
        public const string CountryCodeKey = "CountryCode";

        Dictionary<string, object> metadata = null;

        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;
        INotificationLogger logger;

        public INotificationDelivery Delivery => delivery;

        public INotificationValidator Validator => validator;

        public INotificationFormatted Formatter => formatter;

        public INotificationLogger Logger => logger;

        public SmsNotificationPipeline(
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
                    statusMessage = "Validation failed (Invalid/Missing Phone Number)";
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);

                success = true;
                statusMessage = "SMS Handed off to Carrier Gateway";
                return true;
            }
            catch (Exception ex) {
                statusMessage = $"SMS Gateway Exception: {ex.Message}";
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
                { CountryCodeKey, "+1" }, // Default to US/Canada, for example
                { PhoneNumberKey, "1234567890" }
            };

            return metadata;
        }
    }
}