using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Validators {
    [CreateAssetMenu(fileName = "SlackValidator", menuName = "NotificationSystem/Slack/Validation", order = 0)]
    public class SlackValidator : BaseNotificationValidator {
        public override Task<bool> Validate(in NotificationRequest request) {
            request.MetaData.TryGetValue(SlackNotificationPipeline.SlackChannelKey, out var channel);

            if (channel == null || string.IsNullOrWhiteSpace(channel.ToString())) {
                Debug.LogError("[SlackValidator] Target channel not found in metadata");
                return Task.FromResult(false);
            }
            
            Debug.Log("[SlackValidator] Validation successful");
            return Task.FromResult(true);
        }
    }
}

