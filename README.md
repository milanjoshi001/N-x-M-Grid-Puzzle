# 2048 - Unity

A clean and modular implementation of the classic **2048 puzzle game** built using **Unity 6 and C#**.

The project focuses on separating **game logic, game state, input, and presentation**, creating a maintainable architecture that can be extended with features such as animations, undo, scoring, touch controls, and game-over detection.

---

## 🎮 Game Overview

2048 is a grid-based puzzle game where the player moves numbered tiles in four directions.

When two tiles with the same value collide, they merge into a single tile with double the value.

```text
2 + 2     = 4
4 + 4     = 8
8 + 8     = 16
16 + 16   = 32
...
1024 + 1024 = 2048
```

The objective is to create the **2048 tile** while keeping the board from becoming completely blocked.

---

## ✨ Features

- 4 × 4 game board
- Up / Down / Left / Right movement
- Keyboard controls - W,A,S,D
- Tile movement and compression
- Tile merging
- Random tile spawning
- 90% chance of spawning a `2`
- 10% chance of spawning a `4`
- Unique tile IDs
- Score tracking
- Move counter
- Game-over detection
- Restart functionality
- Undo functionality
- Board state cloning
- Separate game logic and visual representation
- Responsive UI structure

---

## 🏗️ Architecture

The project follows a simple separation-of-concerns architecture.

```text
                         PLAYER
                           │
                           ▼
                    ┌─────────────┐
                    │    Input    │
                    └──────┬──────┘
                           │
                           ▼
                    ┌─────────────┐
                    │ Controller  │
                    └──────┬──────┘
                           │
                           ▼
                    ┌─────────────┐
                    │    Grid     │
                    │             │
                    │ Move()      │
                    │ Merge()     │
                    │ Spawn()     │
                    │ CanMove()   │
                    └──────┬──────┘
                           │
                           ▼
                    ┌─────────────┐
                    │    Board    │
                    │             │
                    │ Tile[,]     │
                    │ Score       │
                    │ Moves       │
                    │ Game State  │
                    └──────┬──────┘
                           │
                           ▼
                    ┌─────────────┐
                    │ GridVisual  │
                    └──────┬──────┘
                           │
              ┌────────────┴────────────┐
              ▼                         ▼
       Cell Container            Tile Container
              │                         │
              ▼                         ▼
         Empty Cells              Tile Visuals
```

### Design Principle

The core game logic does not depend on Unity GameObjects.

The board can therefore be manipulated independently of the visual representation.

```text
Game Logic
    ↓
Board / Grid / Tile
    ↓
Controller
    ↓
Visual Representation
    ↓
Unity UI
```

---

# 🧩 Core Systems

## Board

`Board` represents the current state of the game.

```csharp
public class Board
{
    public Tile[,] Tiles { get; private set; }

    public int Score { get; private set; }

    public int Moves { get; private set; }

    public bool IsGameOver { get; private set; }
}
```

### Responsibilities

- Store the current tile layout
- Store score
- Store move count
- Store game state
- Create board snapshots for undo

The `Board` class contains game data but does not know anything about Unity UI.

---

## Tile

`Tile` represents an individual numbered tile.

```csharp
public class Tile
{
    public int ID { get; private set; }

    public int Value { get; private set; }
}
```

Each tile has a unique ID.

Example:

```text
Tile
├── ID = 12
└── Value = 64
```

The unique ID allows the visual layer to identify a specific tile independently of its position.

---

## Grid

`Grid` contains the core 2048 gameplay rules.

### Responsibilities

- Create the board
- Spawn random tiles
- Move tiles
- Compress tiles
- Merge tiles
- Detect possible moves
- Detect game over

Example:

```csharp
public bool Move(Board board,MoveDirection direction)
{
    ...
}
```

The `Grid` operates on the `Board` data rather than Unity GameObjects.

---

## MoveDirection

Movement is represented using an enum.

```csharp
public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}
```

This allows the input system to remain independent of the actual input device.

---

## GridVisual

`GridVisual` is responsible for converting the board state into Unity visuals.

```text
Board
  ↓
GridVisual
  ↓
Unity GameObjects
```

The visual system does not determine whether a move is valid.

Its responsibility is to display the current board state.

---

## TileVisual

`TileVisual` represents an individual tile in the Unity scene.

Responsibilities include:

- Displaying tile values
- Updating tile appearance
- Managing the tile UI
- Preparing tiles for movement and merge animations

Example:

```csharp
public void Initialize(Tile tile)
{
    TileId = tile.ID;

    _tileValueText.SetText(
        tile.Value.ToString()
    );
}
```

---

## Controller

`Controller` coordinates the different gameplay systems.

The general gameplay flow is:

```text
Player Input
     ↓
Controller
     ↓
Grid.Move()
     ↓
Board Updated
     ↓
Spawn New Tile
     ↓
Check Game Over
     ↓
Render Board
```

The controller acts as the bridge between the input system, gameplay logic, and visual systems.

---

## Input

The input system converts keyboard, mouse, or touch input into a `MoveDirection`.

```text
Arrow Left
    ↓
MoveDirection.Left

Swipe Right
    ↓
MoveDirection.Right

Swipe Up
    ↓
MoveDirection.Up
```

This allows the gameplay system to remain independent from the input device.

---

# 🎲 Board Initialization

The game starts with an empty 4 × 4 board.

```text
[ ][ ][ ][ ]
[ ][ ][ ][ ]
[ ][ ][ ][ ]
[ ][ ][ ][ ]
```

Two random tiles are then spawned.

Example:

```text
[ ][ ][ ][ ]
[ ][2][ ][ ]
[ ][ ][ ][ ]
[ ][ ][4][ ]
```

---

# 🎯 Random Tile Spawning

A new tile is spawned after every successful move.

The system first finds all empty cells.

```csharp
if (board.Tiles[x, y] == null)
{
    emptyCells.Add(new Vector2Int(x, y));
}
```

A random empty position is selected.

The tile value is generated using:

```csharp
int value = Random.value < 0.9f ? 2 : 4;
```

Therefore:

```text
90% → 2
10% → 4
```

A new tile is spawned only after a successful move.

---

# 🔄 Movement System

The movement system processes the board one row or column at a time.

For example:

```text
[ ][2][ ][2]
```

First, empty cells are compressed:

```text
[2][2]
```

Then matching tiles are merged:

```text
[4]
```

Finally, the result is written back into the board:

```text
[4][ ][ ][ ]
```

---

# 🔗 Merge Rules

The implementation follows standard 2048 merge rules.

### Two matching tiles

```text
[2][2][ ][ ]
```

becomes:

```text
[4][ ][ ][ ]
```

---

### Four matching tiles

```text
[2][2][2][2]
```

becomes:

```text
[4][4][ ][ ]
```

It does not become:

```text
[8][ ][ ][ ]
```

---

### Three matching tiles

```text
[2][2][2][ ]
```

becomes:

```text
[4][2][ ][ ]
```

The newly merged tile cannot merge again during the same move.

---

# 🚫 Invalid Moves

A move is considered invalid if the board does not change.

For example:

```text
[2][4][8][16]
```

If the player attempts to move right while all tiles are already positioned against the right side, the board remains unchanged.

The movement system returns:

```csharp
false
```

No move is counted and no new tile is spawned.

---

# 💀 Game Over Detection

A full board does not automatically mean Game Over.

Game Over occurs only when:

1. There are no empty cells.
2. No horizontally adjacent tiles can merge.
3. No vertically adjacent tiles can merge.

Example:

```text
[  2 ][  4 ][  8 ][ 16 ]
[ 32 ][ 64 ][128 ][256 ]
[  2 ][  4 ][  8 ][ 16 ]
[ 32 ][ 64 ][256 ][512 ]
```

If no neighboring tiles have the same value and there are no empty cells:

```text
GAME OVER
```

However:

```text
[  2 ][  4 ][  8 ][ 16 ]
[ 32 ][ 64 ][128 ][256 ]
[  2 ][  4 ][  8 ][ 16 ]
[ 32 ][ 64 ][128 ][128 ]
```

is not Game Over because the two `128` tiles can still merge.

---

# ↩️ Undo System

Undo is implemented using board snapshots.

Before a successful move, the current board is cloned and stored.

```text
Current Board
      │
      ▼
   Clone()
      │
      ▼
 Undo Stack
```

The move is then executed.

```text
Board
  ↓
Move
  ↓
Merge
  ↓
Spawn New Tile
```

When Undo is triggered:

```text
Undo Stack
     │
     ▼
Previous Board
     │
     ▼
Restore Board
```

The snapshot contains the board state before the move, allowing the entire game state to be restored.

Example:

```text
Move 1
   ↓
Move 2
   ↓
Move 3
   ↓
Undo
   ↓
Restore Move 2
```

The board, tile positions, score, and move count can be restored together.

---

# 📊 Score System

When two tiles merge, the resulting tile value is added to the score.

Example:

```text
2 + 2 = 4
```

Score:

```text
+4
```

Another example:

```text
128 + 128 = 256
```

Score:

```text
+256
```

The score therefore represents the total value generated through tile merges.

---

# 🔢 Move Counter

Every successful movement increments the move counter.

Invalid moves do not increase the counter.

Example:

```text
Valid Move
    ↓
Moves++

Invalid Move
    ↓
No change
```

---

# 🖥️ Tile Visuals

Tile appearance changes according to its value.

Example:

```text
2
4
8
16
32
64
128
256
512
1024
2048
```

Each value can have its own visual style, allowing the player to quickly identify high-value tiles.

Tile values are displayed using **TextMeshPro**.

---

# 🎮 Controls

## Keyboard

| Input | Action |
|---|---|
| ↑ / W | Move Up |
| ↓ / S | Move Down |
| ← / A | Move Left |
| → / D | Move Right |

---

# 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| Unity 6 | Game Engine |
| C# | Gameplay Programming |
| Unity Input System | Player Input |
| Unity UI | Interface |
| Git | Version Control |

---

# 🚀 Getting Started

## Requirements

- Unity 6
- Unity Hub
- Git
- Visual Studio, JetBrains Rider, or VS Code

## Installation

Clone the repository:

```bash
git clone <repository-url>
```

Open the project using Unity Hub.

Select the Unity version used by the project.

Open the main scene:

```text
Assets/Scenes/SampleScene.unity
```

Press **Play** in Unity.

---

# 🧪 Testing

Important gameplay scenarios to test include:

### Basic Movement

```text
[2][ ][ ][ ]
```

Move Right:

```text
[ ][ ][ ][2]
```

---

### Compression

```text
[2][ ][ ][2]
```

Move Left:

```text
[4][ ][ ][ ]
```

---

### Multiple Merges

```text
[2][2][2][2]
```

Move Left:

```text
[4][4][ ][ ]
```

---

### Three Matching Tiles

```text
[2][2][2][ ]
```

Move Left:

```text
[4][2][ ][ ]
```

---

### Invalid Movement

```text
[2][4][8][16]
```

Attempting to move right should not change the board or spawn a new tile if the board is already positioned against the right side.

---

### Game Over

Fill the board with values that have no adjacent matches.

The Game Over state should activate.

---

### Undo

```text
Initial State
      ↓
    Move
      ↓
New Random Tile
      ↓
    Undo
      ↓
Previous State
```

The previous board state should be restored exactly.

---

# 🧠 Design Principles

## Separation of Concerns

Game logic is separated from the Unity presentation layer.

```text
Game Logic
    ≠
Presentation
```

The `Grid`, `Board`, and `Tile` classes do not need to know how the UI is rendered.

---

## Single Responsibility

Each major class has a focused responsibility.

```text
Grid
→ Game rules

Board
→ Game state

Tile
→ Tile data

Controller
→ Game flow

Input
→ Player input

GridVisual
→ Board presentation

TileVisual
→ Tile presentation
```

---

## Data-Driven Game State

The board state is represented using:

```csharp
Tile[,]
```

rather than using Unity GameObjects as the source of truth.

This makes the game state easier to:

- Test
- Save
- Load
- Undo
- Simulate
- Debug
- Extend

---

# 📐 Gameplay Data Flow

A complete player action follows this flow:

```text
                  PLAYER
                    │
                    ▼
                  INPUT
                    │
                    ▼
               CONTROLLER
                    │
                    ▼
                GRID.MOVE()
                    │
                    ▼
             BOARD UPDATED
                    │
                    ▼
             ADD RANDOM TILE
                    │
                    ▼
              CAN MOVE?
               /      \
             YES       NO
              │         │
              │      GAME OVER
              │
              ▼
          GRID VISUAL
              │
              ▼
        UPDATE UI / TILES
```

---

# 🔮 Future Improvements

The project architecture is designed to support additional features.

- [ ] Smooth tile movement animations
- [ ] Tile merge animations
- [ ] Tile spawn animations
- [ ] Tile pop effects
- [ ] Persistent TileVisual instances
- [ ] Improved tile-to-visual mapping
- [ ] Multi-level undo history
- [ ] Undo animations
- [ ] Game-over animation
- [ ] Win state when reaching 2048
- [ ] Continue playing after reaching 2048
- [ ] Best score persistence
- [ ] Save / Load system
- [ ] Player statistics
- [ ] Sound effects
- [ ] Haptic feedback
- [ ] Responsive mobile UI
- [ ] Additional board sizes
- [ ] Unit tests for movement and merging
- [ ] Automated gameplay tests
- [ ] Object pooling for tile visuals

---

# 🎯 Project Goals

The main goals of this project are:

1. Implement the complete 2048 gameplay loop.
2. Build the game using clean and maintainable C#.
3. Separate game logic from Unity presentation.
4. Support keyboard and mobile swipe input.
5. Implement reliable tile movement and merging.
6. Implement undo through board state snapshots.
7. Detect valid moves and Game Over conditions.
8. Create an architecture that can support animations and additional features.
9. Practice scalable Unity gameplay architecture.

---

# 📜 License

This project is intended for educational and portfolio purposes.

If you plan to distribute or modify the project, add the appropriate license here.

Example:

```text
MIT License
```

---

# 👨‍💻 Author

**Milan Joshi**

Game Developer / Unity Developer

---

## ⭐ Project Status

This project is currently under development.

The core gameplay systems are being implemented first, followed by visual polish, animation, UI improvements, and additional gameplay features.

```text
Build → Test → Debug → Improve → Repeat
```
