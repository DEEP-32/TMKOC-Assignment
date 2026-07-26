using System;
using System.Collections.Generic;
using System.Linq; // Added for FirstOrDefault lookup
using NotificationSystem.Runtime.Factory;
using NotificationSystem.Runtime.Pipeline.Config;
using NotificationSystem.Runtime.UI;
using UnityEngine;

namespace NotificationSystem.Runtime.Controller {
    
    public class NotificationController : MonoBehaviour {
        
        // MVC Dependencies
        NotificationPipelineConfig modelConfig;
        NotificationDemoUI view;
        INotificationPipelineFactory factory;
        
        // Controller State
        List<NotificationRequest> scheduledRequests;

        private static readonly string DummyMessage = "Hello World from the MonoBehaviour Controller!";

        public void Init(
            NotificationPipelineConfig modelConfig, 
            NotificationDemoUI view, 
            INotificationPipelineFactory factory) 
        {
            this.modelConfig = modelConfig;
            this.view = view;
            this.factory = factory;
            
            scheduledRequests = new List<NotificationRequest>();

            // Bind the View
            this.view.Init(this.modelConfig.Pipelines);
            this.view.TriggerPipeline += OnViewTriggeredPipeline;
        }

        private void OnViewTriggeredPipeline(string pipelineType) {
            
            var pipeline = factory.CreatePipeline(pipelineType);
            if (pipeline == null) {
                Debug.LogError($"[Controller] Failed to create pipeline: {pipelineType}");
                return;
            }

            // 1. Fetch the config entry using the pipelineType
            var configEntry = modelConfig.Pipelines.FirstOrDefault(p => p.Type == pipelineType);
            
            // 2. Safely extract the name (fallback to the Type string if Name is empty)
            string pipelineName = configEntry != null && !string.IsNullOrEmpty(configEntry.Name) 
                ? configEntry.Name 
                : pipelineType;

            DateTime scheduledTime = DateTime.Now.AddSeconds(3); // 3-second delay
            var metaData = pipeline.CreatePipelineMetadata();
            var request = new NotificationRequest(pipelineType, DummyMessage, scheduledTime, metaData);

            if (scheduledTime <= DateTime.Now) {
                _ = pipeline.StartNotificationPipeline(request);
            } else {
                scheduledRequests.Add(request);
                
                // 3. Use the extracted Name in your debug log
                var currentTime = DateTime.Now;
                Debug.Log($"[Controller] Scheduled '{pipelineName}' pipeline for {scheduledTime:T} , current time {currentTime:T} , so starting in : {scheduledTime-currentTime}");
            }
        }

        private void Update() {
            if (scheduledRequests == null || scheduledRequests.Count == 0) return;

            for (int i = scheduledRequests.Count - 1; i >= 0; i--) {
                var request = scheduledRequests[i];
                
                if (DateTime.Now >= request.ScheduledAt) {
                    scheduledRequests.RemoveAt(i);
                    
                    var pipeline = factory.CreatePipeline(request.Type);
                    if (pipeline != null) {
                        _ = pipeline.StartNotificationPipeline(request);
                    }
                }
            }
        }

        private void OnDestroy() {
            if (view != null) {
                view.TriggerPipeline -= OnViewTriggeredPipeline;
            }
        }
    }
}