using System.Threading.Tasks;

namespace WorkflowEngine.Runtime.Services.MockService {
    public class MockAuthService : IAuthService {
        public async Task<AuthResult> LoginWithDeviceIDAsync(string deviceId) {
            await Task.Delay(1000);
            
            return new AuthResult 
            {
                IsSuccessful = true,
                SessionToken = "mock_token_abc123",
                PlayerId = "player_001"
            };
        }
        public async Task<AuthResult> LoginWithEmailAsync(string email, string password) {
            await Task.Delay(1000);
            
            return new AuthResult 
            {
                IsSuccessful = true,
                SessionToken = "mock_token_abc123",
                PlayerId = "player_001"
            };
            
        }
        public void Logout() {
        }
    }
}