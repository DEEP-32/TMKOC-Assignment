using System;
using System.Collections.Generic;

namespace NotificationSystem.Runtime {
    public class NotificationRequest {
        //for internall identifying which classes to use
        public string Type { get; private set; }
        
        public string Name { get; private set; }
        
        public string Message { get; set; }
        public DateTime? ScheduledAt {get; private set;}
        public Dictionary<string, object> MetaData;
        
        
        public NotificationRequest(string type,string name, string message, DateTime? scheduledAt, Dictionary<string, object> metaData) {
            Type = type;
            Name = name;
            Message = message;
            ScheduledAt = scheduledAt;
            MetaData = metaData;
        }
        
        public NotificationRequest(string type, string message) {
            Type = type;
            Message = message;
            ScheduledAt = null;
            MetaData = null;

        }
    }
}