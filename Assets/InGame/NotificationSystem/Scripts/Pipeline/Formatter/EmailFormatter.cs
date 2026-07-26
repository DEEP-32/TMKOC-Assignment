using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Formatter {
    [CreateAssetMenu(fileName = "EmailFormatter", menuName = "NotificationSystem/Email/Formatter", order = 0)]
    public class EmailFormatter : BaseNotificationFormatter {
        public override string Format(in NotificationRequest request) {
            Debug.Log("[EmailFormatter] Formatting email");
            return request.Message;
        }
    }
}