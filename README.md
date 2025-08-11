# 🚀 Three Lanes Runner Game

This is a **3D endless runner** game developed in **Unity** using **C#** scripts, focused on a modular and scalable gameplay system.  
The game takes place on **three lanes** (left, middle, and right), where obstacles and objects spawn dynamically using a custom **SpawnPoint** system.

---

## 🕹️ Game Concept

In *Three Lanes Runner*, the player continuously runs forward through an endless path divided into three lanes. The challenge lies in avoiding **"inevitable" obstacles** — specifically **walls** and **holes** — that require the player to cleverly **change spatial dimensions** to survive.  

- **Dimension-based gameplay:**  
  The game world shifts between different planes (for example, X/Z vs Y/Z axes), altering the player's perspective and movement.  
- **Dynamic rule sets:**  
  Three categories of gameplay rules continuously and randomly change during the run:  
  1. **Camera rules** (e.g., follow-behind, top-down, side view)  
  2. **Movement rules** (e.g., normal controls, inverted controls)  
  3. **Environment rules** (e.g., foggy or clear visibility)  
- **Player control over camera:**  
  Players can manually switch camera views (using keys 1, 2, 3) to better avoid dimension-specific obstacles like walls and holes, adapting their strategy on the fly.  
- **Constant obstacle evasion:**  
  Players must constantly dodge obstacles, balancing quick reflexes with spatial awareness, while rules and environment change dynamically to keep gameplay fresh and challenging.

---

## 🛠️ Technologies Used

- **Unity Engine** (version X.X.X) – primary development platform for the game.  
- **C#** – scripting language powering gameplay logic and systems.  
- **Unity Event System** – to decouple game systems via event-driven architecture.  
- **Prefab System** – for easy instantiation and management of game objects.  
- **ScriptableObjects** – configuration assets for flexible gameplay parameter tuning without code changes.  
- **Unity Animator & Animation System** – for player and object animations.  
- **Unity Physics (Colliders & Rigidbody)** – for collision detection and trigger events.  

---

## 🎮 Features

1. **Three lanes gameplay**  
   - Each lane has an associated `SpawnPoint` component.  
   - Only two lanes are active simultaneously, while one remains inactive to maintain game balance and avoid impossible scenarios.  

2. **Obstacle spawning system**  
   - Dynamic spawning of obstacles ensuring at least two obstacles per Z coordinate on active lanes.  
   - Prevents spawning obstacles on inactive lanes or overlapping in the same lane at the same depth.  
   - Supports extension for complex spawning patterns.  

3. **Dynamic rule swapping**  
   - Rules from three categories (Camera, Movement, Environment) randomly change at intervals.  
   - Visual warnings appear before rule changes, allowing players to prepare.  
   - Changes affect gameplay and environment in real-time.  

4. **Player movement and controls**  
   - Horizontal lane switching based on user input (keyboard or swipe).  
   - Forward running speed with smooth transitions.  
   - Camera modes switchable via keys 1, 2, 3 for strategic advantage.  

5. **UI & HUD**  
   - Displays current score, distance traveled, and collected items.  
   - Start menu and game over screens with restart options.  
   - Countdown and warning UI for upcoming rule changes.  

6. **Event-driven architecture**  
   - Centralized **GameManager** manages game state and flow.  
   - Custom events allow communication between gameplay modules without tight coupling.  
   - Modular and maintainable codebase.  

7. **Prefab and pooling system**  
   - Object pooling to optimize performance by reusing obstacle objects.  
   - Minimizes runtime instantiation and destruction overhead.  

---

## 🧩 Project Architecture

```plaintext
Assets/
├── Scripts/
│   ├── GameManager.cs
│   ├── SpawnPoint.cs
│   ├── ObstacleSpawner.cs
│   ├── PlayerController.cs
│   ├── RuleManager.cs
│   ├── UI/
│   │   ├── StartMenuManager.cs
│   │   ├── GameOverUIManager.cs
│   │   └── RuleChangingUI.cs
│   └── Events/
│       ├── GameEvents.cs
│       └── EventListeners.cs
├── Prefabs/
│   ├── Player.prefab
│   ├── Obstacle.prefab
│   ├── SpawnPoint.prefab
│   └── Lane.prefab
├── Scenes/
│   └── MainScene.unity
└── README.md
