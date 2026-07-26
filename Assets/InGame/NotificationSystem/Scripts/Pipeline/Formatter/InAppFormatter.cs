using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Formatter {
    [CreateAssetMenu(fileName = "InAppFormatter", menuName = "NotificationSystem/InApp/Formatter", order = 0)]
    public class InAppFormatter : BaseNotificationFormatter {
        public override string Format(in NotificationRequest request) {
            Debug.Log("[InAppFormatter] Injecting rich text tags for Unity UI");
            // Example: Highlighting the message for TMP_Text
            return $"<b><color=#FFD700>Notice:</color></b> {request.Message}";
        }
    }
}