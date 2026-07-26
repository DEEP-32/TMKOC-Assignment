using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Delivery {
    [CreateAssetMenu(fileName = "PushDelivery", menuName = "NotificationSystem/Push/Delivery", order = 0)]
    public class PushDelivery : BaseNotificationDelivery {
        public override Task Send(string message, in NotificationRequest request) {
            request.MetaData.TryGetValue(PushNotificationPipeline.DeviceTokenKey, out var token);
            request.MetaData.TryGetValue(PushNotificationPipeline.BadgeCountKey, out var badgeCount);
            
            Debug.Log($"[PushDelivery] Handing off to iOS/Android native APIs... Token: {token}, Badge: {badgeCount}");
            return Task.CompletedTask;
        }
    }
}