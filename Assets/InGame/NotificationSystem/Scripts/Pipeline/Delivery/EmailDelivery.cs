using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Delivery {
    
    [CreateAssetMenu(fileName = "EmailDelivery" ,menuName = "NotificationSystem/Email/Delivery")]
    public class EmailDelivery : BaseNotificationDelivery {
        public override Task Send(string message, in NotificationRequest request) {
            Debug.Log("[EmailDelivery] Sending email");
            return Task.CompletedTask;
        }
    }
}