# Smooth Character Control

## Overview
This script provides smooth movement and jumping mechanics for a 2D Unity game, featuring:
- **Variable jump height** based on button hold duration.
- **Coyote time** for more forgiving jumps.
- **Smooth acceleration and deceleration** for natural movement.
- **Automatic character flipping** based on movement direction (optional).

## Setup
1. Attach `SmoothCharacterControl.cs` to your character GameObject.
2. Ensure it has a **Rigidbody2D** and **Collider2D**.
3. Mark the ground objects with the `Floor` tag.
4. Configure input mappings in [Unity's Input Manager](https://docs.unity3d.com/Manual/class-InputManager.html).

## Features
### Movement
- Uses **horizontal damping** for smooth acceleration/deceleration.
- Adjustable movement speed via `MovementSettings`.
- Character flips based on movement direction.

### Jumping
- **Coyote Time**: Allows jumps shortly after leaving the ground.
- **Jump Buffering**: Press jump slightly before landing to still trigger a jump.
- **Variable Height**: Releases early = shorter jump.
- **Controlled Fall**: Increased gravity after peak for a natural descent.

## Key Settings
#### **MovementSettings**
| Parameter           | Description                        | Default |
|-------------------|--------------------------------|---------|
| `horizontalDamping` | Smoothness of movement changes | `0.1f`  |
| `acceleration`      | Character's max speed          | `10f`   |

#### **JumpSettings**
| Parameter                        | Description                                      | Default |
|----------------------------------|------------------------------------------------|---------|
| `CoyoteTime`                     | Extra jump window after leaving ground         | `0.15f` |
| `MinJumpMultiplier`               | Jump reduction when releasing early            | `0.5f`  |
| `SpeedToFallOnceReachedMaxHeight` | Gravity boost after peak jump                  | `2.5f`  |

## How It Works
- **Movement**: Reads horizontal input, applies SmoothDamp for fluid motion.
- **Jumping**: Handles coyote time, jump buffering, and variable height.

## Modifications
- Adjust `Jumpforce` to tweak jump height.
- Modify `MovementSettings.acceleration` for speed changes.
- Tune `JumpSettings` to refine jump feel.

## Requirements
- Unity **2020+**

## Contributing
Fork, modify, and submit a PR if you'd like to improve the script!

---
### Author
Developed by **Gustavo Segato Gazzeta**.

