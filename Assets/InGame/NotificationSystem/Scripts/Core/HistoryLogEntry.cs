using System.Collections.Generic;
using System.Linq;

namespace NotificationSystem.Runtime.Core {
    [System.Serializable]
    public class HistoryLogEntry {
        public string Timestamp;
        public string PipelineType;
        public string Status;
        public string Message;

        // Newtonsoft will automatically turn this into a nested JSON object!
        public Dictionary<string, object> MetaData;
        
        public override string ToString() {
            
            // 1. Determine the color based on the status string
            string statusColor = Status == "SUCCESS" ? "#00FF00" : "#FF0000"; // Green or Red
            
            // 2. Format the MetaData dictionary into a readable single line
            string metaString = "None";
            if (MetaData != null && MetaData.Count > 0) {
                metaString = string.Join(" | ", MetaData.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            }

            // 3. Construct and return the final Rich Text string
            return $"<b><color={statusColor}>[{PipelineType} - {Status}]</color></b>\n" +
                   $"<b>Time:</b> {Timestamp}\n" +
                   $"<b>Info:</b> <i>{Message}</i>\n" +
                   $"<b>Data:</b> {metaString}";
        }
    }
}