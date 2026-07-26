using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Delivery {
    [CreateAssetMenu(fileName = "InAppDelivery", menuName = "NotificationSystem/InApp/Delivery", order = 0)]
    public class InAppDelivery : BaseNotificationDelivery {
        public override Task Send(string message, in NotificationRequest request) {
            request.MetaData.TryGetValue(InAppNotificationPipeline.DisplayDurationKey, out var duration);
            
            float time = duration is float f ? f : 3.0f; // Fallback to 3 seconds
            Debug.Log($"[InAppDelivery] Spawning UI Toast prefab for {time} seconds: {message}");
            
            // In a real game, you would call your UI Manager here!
            return Task.CompletedTask;
        }
    }
}