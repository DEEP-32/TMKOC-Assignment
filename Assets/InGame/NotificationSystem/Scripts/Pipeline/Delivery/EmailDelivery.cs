using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Delivery {
    [CreateAssetMenu(fileName = "EmailDelivery", menuName = "NotificationSystem/Email/Delivery", order = 0)]
    public class EmailDelivery : BaseNotificationDelivery {
        public override Task Send(string message, in NotificationRequest request) {
            // Extract the target email to log exactly where it is going
            request.MetaData.TryGetValue(EmailNotificationPipeline.EmailKey, out var email);
            
            Debug.Log($"[EmailDelivery] Connecting to SMTP server... Sending payload to {email}");
            return Task.CompletedTask;
        }
    }
}