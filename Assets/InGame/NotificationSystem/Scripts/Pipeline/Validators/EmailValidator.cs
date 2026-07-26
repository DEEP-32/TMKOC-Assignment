using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Validators {
    [CreateAssetMenu(fileName = "EmailValidator", menuName = "NotificationSystem/Email/Validation", order = 0)]
    public class EmailValidator : BaseNotificationValidator {
        public override Task<bool> Validate(in NotificationRequest request) {
            request.MetaData.TryGetValue(EmailNotificationPipeline.EmailKey, out var email);

            if (email == null) {
                Debug.LogError("[EmailValidator] Email not found in metadata");
                return Task.FromResult(false);
            }

            if (email is string emailString) {
                if (!emailString.Contains("@")) {
                    Debug.LogError("[EmailValidator] Invalid email format");
                    return Task.FromResult(false);
                }
            }
            
            Debug.Log("[EmailValidator] Validation successful");
            return Task.FromResult(true);
            
        }
    }
}