using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Delivery {
    [CreateAssetMenu(fileName = "SlackDelivery", menuName = "NotificationSystem/Slack/Delivery", order = 0)]
    public class SlackDelivery : BaseNotificationDelivery {
        public override Task Send(string message, in NotificationRequest request) {
            request.MetaData.TryGetValue(SlackNotificationPipeline.SlackChannelKey, out var channel);
            Debug.Log($"[SlackDelivery] Sending web request to channel {channel}: {message}");
            return Task.CompletedTask;
        }
    }
}