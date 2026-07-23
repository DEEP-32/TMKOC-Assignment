using System.Threading.Tasks;

namespace WorkflowEngine.Runtime.Services.MockService {
    public class MockDownloadService : IConfigService {
        public async Task<DownloadResult> DownloadMainConfigAsync(string sessionToken) {
            await Task.Delay(1000);
            return new DownloadResult {
                IsSuccessful = true,
                DownloadedFilePath = "mock_path_abc123",
            };
        }
        public Task<DownloadResult> DownloadAssetBundleAsync(string bundleId, string sessionToken) {
            throw new System.NotImplementedException();
        }
    }
}