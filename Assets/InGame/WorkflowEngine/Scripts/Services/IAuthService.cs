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

    
}