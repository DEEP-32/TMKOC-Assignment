# Task 2 – Notification Processing System

## 📖 Project Overview
This project is a highly scalable, data-driven Notification Processing System built in Unity. It is designed to handle multiple notification channels (Email, SMS, Push, In-App, and Slack) through a modular, replaceable pipeline.

Instead of hardcoding notification logic, the system utilizes ScriptableObjects to allow designers and developers to mix and match behaviors without modifying core C# scripts. It fully supports immediate sending, delayed scheduling, and persistent JSONL-based history logging.

## 🏗️ Architecture Explanation
The architecture follows a strict **Model-View-Controller (MVC)** design paired with a **Component-Based Pipeline**:

*   **The View (`NotificationDemoUI`):** Handles all UI interactions. It is completely decoupled from the backend and communicates purely through C# Events.
*   **The Model (`NotificationPipelineConfig` & `HistoryLogEntry`):** ScriptableObjects and data structs that define the rules, metadata, and history of notifications.
*   **The Controller (`NotificationController`):** A MonoBehaviour that listens to the View, orchestrates the Factory, manages the scheduling queue (`Update` tick), and triggers the pipelines.
*   **The Pipeline (`INotificationPipeline`):** A guaranteed 4-step execution flow (`Validation → Formatting → Delivery → Logging`). Wrapping the execution in a `try/catch/finally` block ensures that even if Delivery fails, the Logging stage safely records the history.

## 🎨 Design Patterns Used
1.  **Strategy Pattern (Heavy Usage):** The pipeline steps (`BaseNotificationValidator`, `BaseNotificationFormatter`, `BaseNotificationDelivery`, `BaseNotificationLogger`) are implemented as independent ScriptableObjects. This allows us to swap a `ConsoleLogger` for a `JsonHistoryLogger` directly in the Unity Inspector without touching code.
2.  **Factory Pattern:** The `NotificationPipelineFactory` generates the correct concrete pipeline (e.g., `EmailNotificationPipeline`) at runtime based on the string ID passed from the UI, injecting the required Strategies into the constructor.
3.  **Observer Pattern:** The UI uses a standard C# `event Action<string> TriggerPipeline` to broadcast user clicks. The Controller subscribes to this, ensuring the UI has zero dependencies on the backend logic.

## 🏛️ SOLID Principles Applied
*   **S - Single Responsibility Principle:** Every class does one thing. The Validator *only* validates; the Scheduler *only* holds pending requests; the Logger *only* writes to the disk.
*   **O - Open/Closed Principle:** The core system is closed for modification but open for extension. To add a new notification type (e.g., WhatsApp), you simply write new ScriptableObjects and add them to the Config list. No existing pipeline code needs to change.
*   **L - Liskov Substitution Principle:** Any script expecting a `BaseNotificationLogger` can seamlessly accept either the `ConsoleLogger` or the `JsonHistoryLogger` without breaking the application.
*   **I - Interface Segregation Principle:** Interfaces are kept small and specific (`INotificationValidator`, `INotificationDelivery`, etc.) rather than having one massive `INotification` interface that forces unused methods on concrete classes.
*   **D - Dependency Inversion Principle:** Concrete pipelines rely on abstractions (interfaces) rather than concrete implementations. The Factory injects these dependencies into the pipelines at runtime.

## 🚀 Future Improvements & Optimizations
While the current architecture strictly adheres to clean software design, scaling this for a live-ops production game would benefit from:
*   **Object Pooling (Memory Optimization):** Currently, the UI instantiates new notification banner prefabs, and the engine creates new `NotificationRequest` objects on the fly. Implementing Object Pooling (via `UnityEngine.Pool`) for these recurring elements would eliminate runtime memory allocations and prevent frame-stuttering Garbage Collection (GC) spikes during heavy notification bursts.
*   **Zero-Allocation String Formatting:** The JSON logging and history systems currently rely on standard C# string interpolation. Switching to a pooled `StringBuilder` (or a library like ZString) would prevent string memory allocations during heavy background disk-writing operations.

## 🚀 How to Add a New Notification Type
Adding a new notification channel requires **zero modifications to existing classes**:
1. Create a new class implementing `INotificationPipeline`.
2. Create new ScriptableObjects inheriting from `BaseNotificationValidator`, `BaseNotificationFormatter`, and `BaseNotificationDelivery`.
3. Right-click in the Unity Editor to create instances of these assets.
4. Add a new element to the `NotificationPipelineConfig` asset and drag your new assets into the slots.

## 💾 JSON Notification History (Logging)
The system features a persistent `JsonHistoryLogger` that utilizes **JSON Lines (JSONL)**. It writes notifications asynchronously to `Application.persistentDataPath/NotificationHistory.jsonl`.
* Because it appends single JSON lines instead of loading an entire JSON array into memory, it is highly optimized for runtime performance.
* The system captures metadata (like target email addresses or phone numbers) and serializes it directly into the log.

## 🎮 Run Instructions
1. **Prerequisites:** Ensure **Newtonsoft.Json** is installed in your Unity project (via Package Manager -> Add package by name: `com.unity.nuget.newtonsoft-json`).
2. Open the `MainScene` (or whatever scene holds the Installer).
3. Ensure the `NotificationSystemInstaller` object has the `PipelineConfig` and `DemoUI` references assigned in the Inspector.
4. Press **Play**.
5. Click the UI buttons to trigger notifications. Watch the Unity Console to see the mocked delivery processes and pipeline status.
6. **To view logs:** Navigate to your OS's `persistentDataPath` (e.g., `%userprofile%\AppData\LocalLow\<CompanyName>\<ProjectName>` on Windows) to view the generated `.jsonl` history file.