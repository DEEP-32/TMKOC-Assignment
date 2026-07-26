using NotificationSystem.Runtime.Core;
using NotificationSystem.Runtime.Pipeline.Concrete;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Formatter {
    [CreateAssetMenu(fileName = "SmsFormatter", menuName = "NotificationSystem/Sms/Formatter", order = 0)]
    public class SmsFormatter : BaseNotificationFormatter {
        public override string Format(in NotificationRequest request) {
            Debug.Log("[SmsFormatter] Stripping rich text for SMS limits");
            request.MetaData.TryGetValue(SmsNotificationPipeline.CountryCodeKey, out var countryCode);
            
            // Example: Appending country code if it exists
            string prefix = countryCode != null ? $"[{countryCode}] " : "";
            return $"{prefix}System Msg: {request.Message}";
        }
    }
}