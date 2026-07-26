using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Validators {
    [CreateAssetMenu(fileName = "InAppValidator", menuName = "NotificationSystem/InApp/Validation", order = 0)]
    public class InAppValidator : BaseNotificationValidator {
        public override Task<bool> Validate(in NotificationRequest request) {
            if (string.IsNullOrWhiteSpace(request.Message)) {
                Debug.LogError("[InAppValidator] Cannot display an empty popup!");
                return Task.FromResult(false);
            }
            
            Debug.Log("[InAppValidator] Validation successful");
            return Task.FromResult(true);
        }
    }
}

namespace NotificationSystem.Runtime.Pipeline.Formatter { }

namespace NotificationSystem.Runtime.Pipeline.Delivery { }