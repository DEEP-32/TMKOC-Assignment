using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Delivery {
    [CreateAssetMenu(fileName = "SmsDelivery", menuName = "NotificationSystem/Sms/Delivery", order = 0)]
    public class SmsDelivery : BaseNotificationDelivery {
        public override Task Send(string message, in NotificationRequest request) {
            request.MetaData.TryGetValue(SmsNotificationPipeline.PhoneNumberKey, out var number);
            Debug.Log($"[SmsDelivery] Pinging Twilio/AWS SNS to send SMS to {number}...");
            return Task.CompletedTask;
        }
    }
}