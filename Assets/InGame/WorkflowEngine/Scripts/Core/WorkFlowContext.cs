using WorkflowEngine.Runtime.Data;

namespace WorkflowEngine.Runtime.Core {
    public class WorkflowContext : ILoginContext, IDownloadContext,IValidateContext {
        public string LoginEmail { get; set; }

        public string LoginPassword { get; set; }

        public string SessionToken { get; set; }

        public string DownloadedConfigPath { get; set; }
        
        public GameData GameData { get; set; }
    }

    public interface ILoginContext {
        string LoginEmail { get; }

        string LoginPassword { get; }

        string SessionToken { get; set; }
    }

    // Only states downloading things need to see this
    public interface IDownloadContext {
        string SessionToken { get; } // Read-only here!

        string DownloadedConfigPath { get; set; }
    }

    public interface IValidateContext {
        GameData GameData { get; set; }
    }
}