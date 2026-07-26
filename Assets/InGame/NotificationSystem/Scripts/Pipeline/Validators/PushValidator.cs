using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Validators {
    [CreateAssetMenu(fileName = "PushValidator", menuName = "NotificationSystem/Push/Validation", order = 0)]
    public class PushValidator : BaseNotificationValidator {
        public override Task<bool> Validate(in NotificationRequest request) {
            request.MetaData.TryGetValue(PushNotificationPipeline.DeviceTokenKey, out var token);

            if (token == null || string.IsNullOrWhiteSpace(token.ToString())) {
                Debug.LogError("[PushValidator] Device token missing. Cannot send push.");
                return Task.FromResult(false);
            }
            
            Debug.Log("[PushValidator] Validation successful");
            return Task.FromResult(true);
        }
    }
}
