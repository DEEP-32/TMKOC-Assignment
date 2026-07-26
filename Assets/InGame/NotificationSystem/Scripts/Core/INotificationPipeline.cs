using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace NotificationSystem.Runtime.Core {
    public interface INotificationPipeline {
        Task<bool> StartNotificationPipeline(NotificationRequest request);
        
        Dictionary<string, object> CreatePipelineMetadata();
        
        INotificationDelivery Delivery { get; }
        INotificationValidator Validator { get; }
        INotificationFormatted Formatter { get; }
        INotificationLogger Logger { get; }
    }

    public interface INotificationValidator {
        public Task<bool> Validate(in NotificationRequest request);
    }

    public interface INotificationFormatted {
        string Format(in NotificationRequest request);
    }
    
    public interface INotificationDelivery {
        Task Send(string message,in NotificationRequest request);
    }

    public interface INotificationLogger {
        Task Log(NotificationRequest request, bool wasSuccessful, string message);
        Task<List<HistoryLogEntry>> ReadHistoryAsync();
    }
    
    public interface INotificationHistoryLogger {
        void LogHistory();
    }

    public abstract class BaseNotificationValidator : ScriptableObject, INotificationValidator {
        public abstract Task<bool> Validate(in NotificationRequest request);
    }
    
    public abstract class BaseNotificationFormatter : ScriptableObject, INotificationFormatted {
        public abstract string Format(in NotificationRequest request);
    }
    
    public abstract class BaseNotificationDelivery : ScriptableObject, INotificationDelivery {
        public abstract Task Send(string message,in NotificationRequest request);
    }
    
    public abstract class BaseNotificationLogger : ScriptableObject, INotificationLogger {
        public abstract Task Log(NotificationRequest request, bool wasSuccessful, string message);
        public abstract Task<List<HistoryLogEntry>> ReadHistoryAsync();
    }
    
}