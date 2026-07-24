using System;
using System.Collections.Generic;

namespace NotificationSystem.Runtime {
    public class NotificationRequest {
        public string Type { get; private set; }
        public string Message { get; private set; }
        public string Recipient { get; private set; }
        public DateTime? ScheduledAt {get; private set;}
        public IReadOnlyDictionary<string, object> MetaData;
        
        
        public NotificationRequest(string type, string message, string recipient, DateTime scheduledAt, Dictionary<string, object> metaData) {
            Type = type;
            Message = message;
            Recipient = recipient;
            ScheduledAt = scheduledAt;
            MetaData = metaData;
        }
        
        public NotificationRequest(string type, string message, string recipient) {
            Type = type;
            Message = message;
            Recipient = recipient;
            ScheduledAt = null;
            MetaData = null;

        }
    }
}