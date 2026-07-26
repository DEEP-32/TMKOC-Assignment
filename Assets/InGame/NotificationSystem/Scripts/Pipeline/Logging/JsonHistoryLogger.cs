using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NotificationSystem.Runtime.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace NotificationSystem.Runtime.Pipeline.Logging {
    [CreateAssetMenu(fileName = "JsonHistoryLogger", menuName = "NotificationSystem/Logging/Json History Logger", order = 1)]
    public class JsonHistoryLogger : BaseNotificationLogger,INotificationHistoryLogger {
        
        [Tooltip("The name of the log file. Use .jsonl (JSON Lines) extension.")]
        [SerializeField] private string fileName = "NotificationHistory.jsonl";
        
        public override async Task Log(NotificationRequest request, bool wasSuccessful, string message) {
            
            // 1. Build the data object
            HistoryLogEntry newEntry = BuildLogEntry();
            
            // 2. Append it to the file
            await WriteEntryToFileAsync(newEntry);
            // Helper 1: Maps the Request to the Log Entry
            HistoryLogEntry BuildLogEntry() {
                return new HistoryLogEntry {
                    Timestamp = DateTime.Now.ToString("G"),
                    PipelineType = request.Name,
                    Status = wasSuccessful ? "SUCCESS" : "FAILED",
                    Message = message,
                    MetaData = request.MetaData
                };
            }

            // Helper 2: Handles the JSON serialization and disk I/O
            async Task WriteEntryToFileAsync(HistoryLogEntry entryData) {
                string filePath = Path.Combine(Application.persistentDataPath, fileName);
                string jsonLine = JsonConvert.SerializeObject(entryData, Formatting.None);
                
                try {
                    using (StreamWriter writer = new StreamWriter(filePath, append: true)) {
                        await writer.WriteLineAsync(jsonLine);
                    }
                    
                    #if UNITY_EDITOR
                    Debug.Log($"[JsonLogger] History saved to: {filePath}");
                    #endif
                } 
                catch (Exception ex) {
                    Debug.LogError($"[JsonLogger] Critical failure writing to JSON file: {ex.Message}");
                }
            }
        }
        
        // --- STREAMING THE JSON BACK TO UNITY ---
        public override async Task<List<HistoryLogEntry>> ReadHistoryAsync() {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            List<HistoryLogEntry> history = new List<HistoryLogEntry>();

            if (!File.Exists(filePath)) return history;

            try {
                using (StreamReader reader = new StreamReader(filePath)) {
                    string line;
                    
                    while ((line = await reader.ReadLineAsync()) != null) {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        var entry = JsonConvert.DeserializeObject<HistoryLogEntry>(line);
                        if (entry != null) {
                            history.Add(entry);
                        }
                    }
                }
            } 
            catch (Exception ex) {
                Debug.LogError($"[JsonLogger] Critical failure reading JSON file: {ex.Message}");
            }

            history.Reverse(); // Put newest notifications at the top of the list
            return history;
        }
    

        public async void LogHistory() {
            var history = await ReadHistoryAsync();
            for (var i = 0; i < history.Count; i++) {
                var currentEntry = history[i];
                Debug.Log($"{currentEntry.ToString()}");
            }
        }

    }
    
}