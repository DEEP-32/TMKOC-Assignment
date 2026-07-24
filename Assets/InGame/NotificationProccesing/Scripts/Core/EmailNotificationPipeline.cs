namespace NotificationSystem.Runtime.Core {
    public class EmailNotificationPipeline : INotificationPipeline {

        public const string EmailDataKey = "EmailDataKey";
        public const string EmailPasswordKey = "EmailPasswordKey";
        
        INotificationDelivery delivery;
        INotificationValidator validator;
        INotificationFormatted formatter;



        public INotificationDelivery Delivery => delivery;
        public INotificationValidator Validator { get; private set;}
        public INotificationFormatted Formatter { get; private set; }
    }
}