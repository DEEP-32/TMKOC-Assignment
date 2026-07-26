using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Formatter {
    [CreateAssetMenu(fileName = "SlackFormatter", menuName = "NotificationSystem/Slack/Formatter", order = 0)]
    public class SlackFormatter : BaseNotificationFormatter {
        public override string Format(in NotificationRequest request) {
            Debug.Log("[SlackFormatter] Formatting Slack JSON payload");
            // Mocking a Slack block-kit JSON string
            return $"{{\"text\": \"*Alert:* {request.Message}\"}}"; 
        }
    }
}