using System.Threading.Tasks;

namespace WorkflowEngine.Runtime.Services {
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