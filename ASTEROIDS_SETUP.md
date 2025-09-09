# Asteroids Game Setup Instructions

## Required Scripts Created:
- `Player.cs` - Handles player movement and collision detection
- `Asteroid.cs` - Manages asteroid movement, types, and visual properties
- `GameManager.cs` - Controls game state, spawning, and respawn functionality

## Setup Steps:

### 1. Create GameObjects in Scene:

**Player Setup:**
1. Create an empty GameObject named "Player"
2. Add a SpriteRenderer component
3. Add a Rigidbody2D component (set to Kinematic)
4. Add a CircleCollider2D component (set as Trigger)
5. Add the Player script
6. Assign a simple sprite (like a square or triangle) to represent the ship
7. Tag the GameObject as "Player"

**Asteroid Setup:**
1. Create an empty GameObject named "Asteroid"
2. Add a SpriteRenderer component
3. Add a Rigidbody2D component (set to Kinematic)
4. Add a CircleCollider2D component (set as Trigger)
5. Add the Asteroid script
6. Assign a simple sprite (like a square or circle) to represent the asteroid
7. Tag the GameObject as "Asteroid"
8. Make this GameObject a Prefab

**GameManager Setup:**
1. Create an empty GameObject named "GameManager"
2. Add the GameManager script
3. Assign the Player and Asteroid prefabs to the script fields

### 2. Camera Setup:
- Set Camera size to 5 (for 2D view)
- Position camera at (0, 0, -10)

### 3. Controls:
- **W/Up Arrow**: Move forward
- **A/Left Arrow**: Rotate left
- **D/Right Arrow**: Rotate right
- **R**: Restart game

### 4. Features Implemented:
✅ Player controls (WASD/Arrow keys)
✅ Multiple asteroid types (Large, Medium, Small) with different colors and speeds
✅ Collision detection between player and asteroids
✅ Player respawn on death (2-second delay)
✅ Screen wrapping for both player and asteroids
✅ Game restart functionality

### 5. Asteroid Types:
- **Large**: Gray, slow movement, large size
- **Medium**: White, medium movement, normal size  
- **Small**: Yellow, fast movement, small size

The game is designed with simple, beginner-friendly code that focuses on the core mechanics required for the assignment.
