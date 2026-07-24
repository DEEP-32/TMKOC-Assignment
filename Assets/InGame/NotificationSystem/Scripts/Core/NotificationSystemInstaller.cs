using NotificationSystem.Runtime.Pipeline.Config;
using TMKOC.Utils;
using UnityEngine;

namespace NotificationSystem.Runtime.Core {
    public class NotificationSystemInstaller : MonoBehaviour {
        [InlineEditor,SerializeField] NotificationPipelineConfig pipelineConfig;
    }
}