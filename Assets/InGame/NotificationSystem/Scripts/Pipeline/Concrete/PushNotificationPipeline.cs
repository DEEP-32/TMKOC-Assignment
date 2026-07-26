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
        
        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator => validator;
        public INotificationFormatted Formatter => formatter;
        
        public PushNotificationPipeline(
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
                    Debug.LogError("[PushPipeline] Missing device token or invalid payload.");
                    return false;
                }

                string formattedMessage = formatter.Format(request);
                
                // This delivery strategy would interface with Unity's Mobile Notifications package
                await delivery.Send(formattedMessage, request); 
                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"[PushPipeline] Push service failure: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, object> CreatePipelineMetadata() {
            if(metadata != null) return metadata;
            
            metadata = new Dictionary<string, object> {
                { BadgeCountKey, 1 },
                { PlaySoundKey, true }
            };
            
            return metadata;
        }
    }
}