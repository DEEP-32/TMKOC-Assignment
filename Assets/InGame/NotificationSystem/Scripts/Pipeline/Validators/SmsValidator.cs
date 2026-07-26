using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Validators {
    [CreateAssetMenu(fileName = "SmsValidator", menuName = "NotificationSystem/Sms/Validation", order = 0)]
    public class SmsValidator : BaseNotificationValidator {
        public override Task<bool> Validate(in NotificationRequest request) {
            request.MetaData.TryGetValue(SmsNotificationPipeline.PhoneNumberKey, out var phoneNumber);

            if (phoneNumber == null || string.IsNullOrWhiteSpace(phoneNumber.ToString())) {
                Debug.LogError("[SmsValidator] Phone number is missing");
                return Task.FromResult(false);
            }
            
            Debug.Log("[SmsValidator] Validation successful");
            return Task.FromResult(true);
        }
    }
}