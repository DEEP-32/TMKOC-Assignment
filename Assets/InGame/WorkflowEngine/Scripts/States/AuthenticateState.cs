using System;
using UnityEngine;
using WorkflowEngine.Runtime.Core;
using WorkflowEngine.Runtime.Services;

namespace WorkflowEngine.Runtime.States {
    public class AuthenticateState : IWorkFlowState {
        
        readonly IAuthService authService;
        readonly ILoginContext loginContext;
        
        public event Action OnSuccess;
        public event Action<string> OnFailure;
        
        public AuthenticateState(IAuthService authService, ILoginContext loginContext) {
            this.authService = authService;
            this.loginContext = loginContext;
        }
        
        public void OnEnter() {
            Debug.Log($"[Workflow] Entering {nameof(AuthenticateState)}...");

        }
        public void OnExit() {
            Debug.Log($"[Workflow] Exiting {nameof(AuthenticateState)}...");

        }
        public async void Execute() {
            try 
            {
                // Read the data driven credentials from the context
                string email = loginContext.LoginEmail;
                string password = loginContext.LoginPassword;

                // Await the asynchronous login process
                var result = await authService.LoginWithEmailAsync(email, password);

                if (result.IsSuccessful) 
                {
                    loginContext.SessionToken = result.SessionToken;
                    Debug.Log($"[Workflow] [state : {nameof(AuthenticateState)}] successful : Session Token: {result.SessionToken}");
                    OnSuccess?.Invoke();
                } 
                else 
                {
                    OnFailure?.Invoke($"Auth Rejected: {result.ErrorMessage}");
                }
            }
            catch (Exception ex) 
            {
                OnFailure?.Invoke($"Auth Exception: {ex.Message}");
            }
            
        }

        
    }
}