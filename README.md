# 3rd-Person Shooter (TPS) Game Foundation

A robust, modular Third-Person Shooter (TPS) foundational project built in Unity. This project implements core mechanics required for a modern 3rd-person action game, including physics-based movement, dynamic camera systems, weapon mechanics, basic enemy AI, and UI integration.

## 🚀 Key Features

* **Responsive 3rd-Person Movement:** Smooth character movement with rotation aligned to the camera's perspective.
* **Dynamic Camera System:** Advanced camera following behavior with integrated screen-shake feedback for impactful combat.
* **Complete Gunplay Mechanics:** Raycast-based shooting system with real-time ammunition tracking and reloading.
* **Modular Health System:** Reusable health scripts for managing damage, death states, and specific player health behaviors.
* **State-Driven Enemy AI:** Basic patrol, chase, and attack behaviors using Unity's NavMesh system.
* **Real-time UI Feedback:** Head-Up Display (HUD) elements tracking live health and ammo status.

---

## 📂 Project Architecture & Script Breakdown

The core logic is divided into modular C# scripts located in the `Assets/Scripts` directory:

### 🎮 Player & Movement
* **`ThirdPersonMovement.cs`**: Handles player inputs, character movement, physics interaction via CharacterController, and aligns player rotation dynamically with the camera orientation.

### 🔫 Weapon & Combat Mechanics
* **`GunShoot.cs`**: Manages the shooting logic using raycasting, fire rates, ammo consumption, reloading states, and triggers camera shake impacts upon firing.

### 🎥 Camera System
* **`CameraFollow.cs`**: Smoothly tracks the player's position and rotation with configurable offsets to maintain an optimal 3rd-person view.
* **`CameraShake.cs`**: Provides cinematic screen-shake effects when taking damage or firing heavy weapons to enhance game feel.

### 🤖 Enemy AI
* **`EnemyAI.cs`**: Implements basic state-machine tracking to handle enemy line-of-sight, chasing mechanics, and transitioning into attack states.
* **`EnemyAttack.cs`**: Defines the specific attack behaviors, cooldown timings, and damage delivery methods when an enemy reaches the player.

### 🧪 Health & Damage Management
* **`Health.cs`**: A generic, reusable base class that handles current health tracking, damage reception, and death triggers for any entity (e.g., destructible objects, enemies).
* **`PlayerHealth.cs`**: Inherits from `Health.cs` or extends specific player-centric logic, updating the player's UI health bar and triggering game-over conditions.

### 📊 User Interface (UI)
* **`AmmoUi.cs`**: Connects directly with the weapon system to display current magazine counts and total reserve ammo on the player's screen.

---

## 🛠️ Getting Started

### Prerequisites
* **Unity Editor:** Version 2021.3 LTS or higher recommended.
* **Unity Packages:** NavMesh Components (if using updated AI packages).

### Installation
1. Clone the repository to your local machine:
   ```bash
   git clone [https://github.com/alirajput1014/3rd-Person-Shooter.git](https://github.com/alirajput1014/3rd-Person-Shooter.git)
