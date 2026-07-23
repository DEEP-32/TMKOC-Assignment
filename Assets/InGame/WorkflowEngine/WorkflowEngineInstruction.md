Task 1 – Workflow Automation Engine
Project Overview

This project implements a highly scalable, data-driven Workflow Engine designed to execute a sequential series of asynchronous operations. Built to enterprise standards, the system safely handles state transitions, asynchronous tasks (such as network authentication and file downloading), and features robust success, failure, and retry handling. The core engine is completely decoupled from the specific states it executes, allowing designers to configure, add, or remove steps in the workflow strictly through data, without modifying existing core classes.
Architecture & Responsibilities

The architecture achieves low coupling and high cohesion by dividing the system into distinct layers:

    The Configuration Layer (Data-Driven Design):
    Workflows are defined entirely via Unity ScriptableObjects. Designers can create a sequence by selecting state types from a custom dropdown in the Inspector and configuring state-specific rules (like max retry attempts). The engine reads this data at runtime, fulfilling the requirement to keep the workflow as data-driven as possible.

    The Execution Layer (State Machine Engine):
    The core engine acts purely as an orchestrator. It knows nothing about the internal logic of individual states; it only listens for their Success or Failure events. When a state succeeds, the engine advances the sequence. When a state fails, the engine queries a Retry Strategy to determine if it should attempt the state again or halt the workflow entirely.

    The Data Pipeline (Context Routing):
    To avoid a monolithic "God Object," data is passed between states using a shared Context object, which is strictly filtered using Interface Segregation. A state that handles downloading can only access download-related data, protecting sensitive information like login credentials.

Design Patterns Used

    State Pattern (Core Requirement): Each step of the workflow (e.g., Authenticate User, Download Configuration, Validate Configuration, Initialize Services) is encapsulated into its own isolated class. This prevents massive conditional statements, simplifies error handling, and makes each step independently testable.

    Factory Pattern (Manual Dependency Injection):

        Why use a Factory for Dependency Injection? Because the workflow sequence is defined by data at runtime (via ScriptableObjects), the system does not know which states it needs to create when the game first boots up. The Factory acts as the manual Dependency Injection container. It reads the configuration data, dynamically instantiates the correct state, and securely passes the necessary long-lived services (like Authentication or Configuration services) into the state's constructor before handing the fully built object to the Engine.

    Observer / Event Pattern: The system relies entirely on event-driven programming. States notify the Engine of their completion via events, and the Engine broadcasts event notifications when entering/exiting states, or upon total workflow success/failure.

    Strategy Pattern: The retry mechanism is abstracted into a Strategy. This allows the engine to easily switch between different retry behaviors (e.g., immediate retry, delayed retry, or exponential backoff) without altering the core engine logic.

SOLID Principles Applied

    Single Responsibility Principle (SRP): Every class has one specific job. The Bootstrapper gathers resources, the Factory builds the states and injects dependencies, the Engine orchestrates the flow, and the States execute their isolated tasks.

    Open/Closed Principle (OCP): The engine and factory are open for extension but closed for modification. Adding a completely new state to the workflow simply requires creating the new state class and registering it in the factory. No core engine or bootstrapper logic needs to change.

    Liskov Substitution Principle (LSP): The engine treats every state identically through a shared contract. Any state can be substituted for another without breaking the engine's execution loop.

    Interface Segregation Principle (ISP): The shared runtime data context is divided into highly specific interfaces. States are injected only with the exact interface they require, adhering to the Principle of Least Privilege.

    Dependency Inversion Principle (DIP): High-level modules do not depend on low-level implementations. States depend entirely on service interfaces, and the Factory injects mock or concrete implementations at runtime.

Run Instructions

    Open the Unity Project and load the Task 1 scene.

    In the Project window, locate the Workflow Definition ScriptableObject to inspect how the workflow sequence (Start → Authenticate User → Download Configuration → Validate Configuration → Initialize Services → Ready) and retry mechanisms are configured.

    Press Play in the Unity Editor.

    Click the "Start Workflow" button located on the UI canvas to trigger the engine.

    Open the Unity Console. You will see detailed logs generated by the Observer events, tracking the engine as it transitions through the states, handles simulated asynchronous operations, and successfully completes the workflow.
