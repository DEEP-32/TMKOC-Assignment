using NotificationSystem.Runtime.Core;

namespace NotificationSystem.Runtime.Factory {
    public interface INotificationPipelineFactory {
        public INotificationPipeline CreatePipeline(string pipelineName);
        
    }
}