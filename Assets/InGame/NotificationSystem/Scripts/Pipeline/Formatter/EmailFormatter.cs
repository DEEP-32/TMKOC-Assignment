using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Formatter {
    [CreateAssetMenu(fileName = "EmailFormatter", menuName = "NotificationSystem/Email/Formatter", order = 0)]
    public class EmailFormatter : BaseNotificationFormatter {
        public override string Format(in NotificationRequest request) {
            Debug.Log("[EmailFormatter] Wrapping message in standard HTML email template...");
            
            // Adding a little flavor to show that formatting actually changes the raw message
            return $"<html><body><p>{request.Message}</p></body></html>"; 
        }
    }
}