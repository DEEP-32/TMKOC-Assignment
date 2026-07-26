using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NotificationSystem.Runtime.UI {
    [RequireComponent(typeof(Button))]
    public class PipelineTrigger : MonoBehaviour {
        public Button Button { get; private set; }
        public TMP_Text Text { get; private set; }
        
        public string PipelineId { get; private set; }
        
        int index;

        void Awake() {
            Button = GetComponent<Button>();
            Text = GetComponentInChildren<TMP_Text>();
        }
        

        public void Init(string pipelineId,string pipelineName,int index,Action<int> onButtonClick) {
            PipelineId = pipelineId;
            Text.text = pipelineName;
            this.index = index;
            
            Button.onClick.AddListener(() => onButtonClick(index));
        }
    }
}