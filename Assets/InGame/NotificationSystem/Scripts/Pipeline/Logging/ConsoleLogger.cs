using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Logging {
    
    [CreateAssetMenu(fileName = "ConsoleLogger", menuName = "NotificationSystem/Logging/Console Logger", order = 0)]
    public class ConsoleLogger : BaseNotificationLogger {
        
        public override Task Log(NotificationRequest request, bool wasSuccessful, string message) {
            
            // Color code based on success
            string color = wasSuccessful ? "#00FF00" : "#FF0000"; // Green or Red
            string status = wasSuccessful ? "SUCCESS" : "FAILED";
            
            string logEntry = $"<b><color={color}>[{request.Name} Pipeline - {status}]</color></b>\n" +
                              $"<b>Time:</b> {DateTime.Now:T}\n" +
                              $"<b>Payload:</b> {request.Message}\n" +
                              $"<b>System Info:</b> {message}";

            if (wasSuccessful) {
                Debug.Log(logEntry);
            } else {
                Debug.LogWarning(logEntry);
            }

            return Task.CompletedTask;
        }

        public override Task<List<HistoryLogEntry>> ReadHistoryAsync() {
            return Task.FromResult(new List<HistoryLogEntry>());
        }
    }
}