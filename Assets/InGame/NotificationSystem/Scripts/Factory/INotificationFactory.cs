using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Factory {
    public interface INotificationFactory {
        public INotificationPipeline CreatePipeline(string pipelineName);
        
    }
}