using System.Collections.Generic;
using NotificationSystem.Runtime.Extensions;
using NotificationSystem.Runtime.Pipeline.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NotificationSystem.Runtime.UI {
    public class NotificationDemoUI : MonoBehaviour {
        [SerializeField] Button buttonPrefab;
        [SerializeField] Transform container;

        public void Init(IReadOnlyList<PipelineConfigEntry> config) {
            foreach (var pipelineConfigEntry in config) {
                var button = Instantiate(buttonPrefab, container);
                button.GetComponentInChildren<TMP_Text>().text = pipelineConfigEntry.Name;
            }
        }
    }
}