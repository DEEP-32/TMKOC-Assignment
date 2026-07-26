using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Pipeline.Concrete {
    public class SmsNotificationPipeline : INotificationPipeline {
        public const string PhoneNumberKey = "PhoneNumber";
        public const string CountryCodeKey = "CountryCode";
        
        Dictionary<string, object> metadata = null;
        
        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;
        
        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator => validator;
        public INotificationFormatted Formatter => formatter;
        
        public SmsNotificationPipeline(
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
                    Debug.LogError("[SmsPipeline] Invalid phone number or payload.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                await delivery.Send(formattedMessage, request);
                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"[SmsPipeline] Delivery failed: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, object> CreatePipelineMetadata() {
            if(metadata != null) return metadata;
            
            metadata = new Dictionary<string, object> {
                { CountryCodeKey, "+1" }, // Default to US/Canada, for example
                { PhoneNumberKey , "1234567890" }
            };
            
            return metadata;
        }
    }
}