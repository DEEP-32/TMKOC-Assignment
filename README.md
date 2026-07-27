# 🛠️ Shared Core Utilities & Project Structure

## 📖 Overview
This repository contains two enterprise-grade architectural systems built in Unity: a **Workflow Automation Engine** and a **Notification Processing System**. 

While both systems are completely independent and decoupled, they share a suite of custom **Unity Editor Utilities**. These utilities bridge the gap between rigid software architecture (like the Strategy and Factory patterns) and a designer-friendly Unity Inspector experience.

---

## ✨ Custom Inspector Utilities

To maintain strict adherence to the **Open/Closed Principle** without forcing designers to write code, this project utilizes custom Property Drawers and Attributes.

### 1. `[TypeDropdown]` Attribute
In highly decoupled systems, classes often rely on interfaces (e.g., `INotificationPipeline`) rather than concrete types. However, Unity cannot natively serialize or display interfaces in the Inspector. 

The `[TypeDropdown(typeof(T))]` attribute solves this by using C# Reflection to scan the assembly for all non-abstract classes that implement a specific interface or base class. 
* **How it works:** It generates a dynamic dropdown menu in the Unity Inspector. 
* **Why it matters:** Designers can select concrete strategies (like `EmailNotificationPipeline` or `AuthenticateUserState`) directly from a dropdown instead of typing error-prone string IDs. This securely feeds the runtime **Factory Pattern** while preventing typos.

### 2. `[InlineEditor]` Attribute
Enterprise architectures heavily utilize **ScriptableObjects** for data-driven configuration. A common UX frustration in Unity is having to constantly click back and forth between a MonoBehaviour and its referenced ScriptableObjects.

* **How it works:** The `[InlineEditor]` attribute forces Unity to draw the serialized properties of a referenced ScriptableObject directly inside the parent MonoBehaviour's Inspector.
* **Why it matters:** In the Notification System, the `NotificationSystemInstaller` uses this attribute so developers can edit the `NotificationPipelineConfig` (and all its sub-pipelines) natively within the Installer's context, drastically speeding up workflow iteration.

---

## 📂 Repository Organization

To maintain high cohesion and prevent cross-contamination of code, the project is divided into distinct, isolated modules. Each major system operates completely independently and contains its own dedicated `README.md` for specific technical documentation.

```text
📁 Assets/
├── 📁 Core_Utilities/          # Contains shared attribute scripts ([TypeDropdown], [InlineEditor])
│   └── 📁 Editor/              # Contains custom PropertyDrawers for the attributes
│
├── 📁 Task1_WorkflowEngine/
│   ├── 📄 README.md            # Task 1 Documentation (Architecture, SOLID, Run Instructions)
│   ├── 📁 Runtime/             # Core Engine, States, and Context
│   ├── 📁 Data/                # Workflow Definitions (ScriptableObjects)
│   └── 📁 Scenes/              # Contains the Task 1 demo scene showcasing the workflow
│
└── 📁 Task2_NotificationSystem/
    ├── 📄 README.md            # Task 2 Documentation (Pipelines, MVC, Factories)
    ├── 📁 Runtime/             # MVC Core, Delivery, Validation, Formatting, Logging
    ├── 📁 Data/                # Pipeline Configs and Concrete Strategies (ScriptableObjects)
    └── 📁 Scenes/              # Contains the Task 2 demo scene showcasing notifications
