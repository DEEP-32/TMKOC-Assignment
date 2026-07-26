using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Formatter {
    [CreateAssetMenu(fileName = "PushFormatter", menuName = "NotificationSystem/Push/Formatter", order = 0)]
    public class PushFormatter : BaseNotificationFormatter {
        public override string Format(in NotificationRequest request) {
            Debug.Log("[PushFormatter] Truncating for OS limits");
            
            // Push notifications get cut off by iOS/Android if they are too long
            string safeMessage = request.Message.Length > 100 
                ? request.Message.Substring(0, 97) + "..." 
                : request.Message;
                
            return safeMessage;
        }
    }
}