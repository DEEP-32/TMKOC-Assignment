using System.Threading.Tasks;

namespace WorkflowEngine.Runtime.Services {
    // A data struct to hold the result of a login attempt
    public struct AuthResult {
        public bool IsSuccessful;
        public string SessionToken;
        public string PlayerId;
        public string ErrorMessage;
    }

    public interface IAuthService {
        Task<AuthResult> LoginWithDeviceIDAsync(string deviceId);
        Task<AuthResult> LoginWithEmailAsync(string email, string password);

        void Logout();
    }

    public struct DownloadResult {
        public bool IsSuccessful;

        // The payload we care about upon success
        public string DownloadedFilePath;
        public string RawJsonData;

        // The error details if it fails
        public string ErrorMessage;
    }

    public interface IConfigService {
        // we require the session token to authorize the download!
        Task<DownloadResult> DownloadMainConfigAsync(string sessionToken);

        // we could add other specific download methods here if needed
        Task<DownloadResult> DownloadAssetBundleAsync(string bundleId, string sessionToken);
    }
}