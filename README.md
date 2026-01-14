# IndividualProject UnityGame ProceduralGeneration
 contain the scource code of the unity game 

# Hell Whole Reloaded

A 2D procedurally-generated dungeon crawler game built in Unity where players battle through ever-changing dungeons, defeat enemies, collect loot, and upgrade their gear.

## 🎮 Game Overview

Hell Whole Reloaded is a roguelike dungeon crawler featuring procedurally generated levels, RPG progression mechanics, and intense combat. Players explore randomly generated dungeons, defeat various enemies, collect coins, and upgrade their equipment to progress through increasingly difficult levels.

## ✨ Features

### Procedural Level Generation
- **Three Generation Algorithms:**
  - **Simple Random Walk**: Creates organic, cave-like dungeon layouts
  - **Corridor First**: Generates corridors first, then adds rooms along the paths
  - **Room First**: Uses Binary Space Partitioning (BSP) to create structured room layouts
- **Dynamic Content Placement:**
  - Automatic enemy spawn point generation
  - Portal placement for level transitions
  - Chest and box spawning
  - Healing area placement
- **Configurable Parameters**: Adjustable room sizes, corridor lengths, and iteration counts via ScriptableObjects

### Combat System
- **Player Combat:**
  - 4-directional movement and attack animations
  - Melee weapon attacks with upgradeable damage
  - Health and experience systems
  - Level-up mechanics with stat improvements
- **Enemy Types:**
  - **Basic Melee Enemies**: Chase and attack the player in close range
  - **Ranged Shooters**: Fire projectiles from a distance
  - **Boss Enemies**: Powerful enemies with increased health and damage
- **Combat Mechanics:**
  - Hitbox-based collision detection
  - Floating damage numbers for visual feedback
  - Knockback effects on hit

### Progression & Economy
- **Experience System**: Gain XP from defeating enemies to level up
- **Coin Collection**: Collect coins from defeated enemies and destroyed boxes
- **Weapon Upgrades**: Spend coins to upgrade weapon damage through multiple tiers
- **Character Stats**: Track health, level, coins, and experience progress
- **Persistent Save System**: Game state saves automatically across sessions

### UI & Menus
- **Main Menu**: Start new game, load saved game, or quit
- **Character Menu**: 
  - View current stats (health, level, coins, experience)
  - Upgrade weapons with visual cost display
  - Real-time stat updates
- **HUD Elements**:
  - Health bar display
  - Experience bar with progress tracking
  - Floating text for damage, pickups, and notifications

### Level Progression
- **Portal System**: Transition between procedurally generated levels
- **Increasing Difficulty**: Enemies become stronger as you progress
- **Breakable Objects**: 
  - Boxes that drop coins and collectibles
  - Chests containing valuable loot
- **Healing Areas**: Strategic healing zones within levels

### Audio
- **Sound Effects**: 
  - Player actions (attack, hit, movement)
  - Enemy actions and damage
  - UI interactions and menu selections
  - Item pickups and interactions
- **Background Music**: Persistent BGM across scenes

## 🛠️ Technologies Used

### Game Engine & Framework
- **Unity 2022.x** - Core game engine
- **C#** - Primary programming language
- **Unity Tilemap** - 2D level rendering system

### Unity Systems & APIs
- **Unity Physics 2D** - Collision detection and physics
- **Unity Animation System** - Character and sprite animations
- **Unity Audio** - Sound effect and music playback
- **Unity UI (uGUI)** - Menu and HUD systems
- **ScriptableObjects** - Data-driven level generation parameters
- **PlayerPrefs** - Save/load system for persistent data

### Design Patterns & Architecture
- **State Machine Pattern** - Enemy AI behavior
- **Object Pooling** - Efficient projectile and enemy management
- **Singleton Pattern** - GameManager for global state management
- **Strategy Pattern** - Multiple level generation algorithms
- **Observer Pattern** - Event-driven UI updates

### Algorithms
- **Random Walk Algorithm** - Organic dungeon generation
- **Binary Space Partitioning (BSP)** - Structured room division
- **Pathfinding** - Enemy AI navigation (basic chase behavior)
- **Procedural Generation** - Dynamic content placement

### Data Structures
- **HashSet<Vector2Int>** - Efficient position tracking for dungeon tiles
- **Queue<BoundsInt>** - Room subdivision in BSP algorithm
- **List<T>** - Path and room storage

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── PGA/                    # Procedural Generation Algorithms
│   ├── Player/                 # Player movement and controls
│   ├── Enemy/                  # Enemy AI and behavior
│   ├── Boss/                   # Boss enemy scripts
│   ├── Fighter/                # Base combat and movement classes
│   ├── Weapon/                 # Weapon systems
│   ├── GameManager/            # Core game management
│   ├── Menu/                   # UI and menu systems
│   ├── Portal/                 # Level transition system
│   ├── Collide/                # Collision and collectibles
│   ├── Text/                   # Floating text system
│   └── Data/                   # ScriptableObject data files
├── Prefabs/                    # Reusable game objects
├── Scenes/                     # Game scenes
├── Sprites/                    # 2D artwork and textures
├── Tiles/                      # Tilemap assets
├── ArtWork/                    # Animations and fonts
├── CasualGameSounds/           # Sound effects library
└── RPG_soundCollection/        # RPG-specific audio

```

## 🎯 Core Gameplay Loop

1. **Spawn** in a procedurally generated dungeon
2. **Explore** the level, discovering enemies and loot
3. **Combat** enemies using melee attacks
4. **Collect** coins and experience from defeated enemies
5. **Upgrade** weapons and stats in the character menu
6. **Progress** through portals to new, more challenging levels
7. **Survive** and continue building your character's strength

## 🚀 Getting Started

### Prerequisites
- Unity 2022.x or later
- Basic understanding of Unity Editor

### Installation
1. Clone or download this repository
2. Open the project in Unity Hub
3. Open the main scene from `Assets/Scenes/`
4. Press Play to start the game

## 🎮 Controls
- **Arrow Keys / WASD** - Move player
- **Space / Left Click** - Attack
- **E** - Interact with objects (chests, NPCs, portals)
- **Tab / ESC** - Open character menu

## 🔮 Future Enhancements
- Additional enemy types and behaviors
- More weapon varieties and special abilities
- Power-ups and temporary buffs
- Multiple character classes
- Procedural item generation
- Boss arenas and special rooms

## 📝 License
This project is for educational and portfolio purposes.

---

**Note**: This is a work-in-progress project showcasing procedural generation, game design, and Unity development skills.
