# Smooth Character Control

## Overview
This script provides smooth character movement and jumping mechanics for a 2D Unity game. It incorporates features such as:
- **Variable jump height** based on how long the jump button is held
- **Coyote time** for forgiving jump timing
- **Smooth acceleration and deceleration** for a butter smooth movement
- **Character rotation on the transform.localScale based on movement direction** - If you have a different spritesheet for the left movement, you can just comment the Rotate() method.

## Features

### Movement
- Uses **horizontal damping** for smooth acceleration and deceleration.
- Adjusts movement speed dynamically based on `MovementSettings`.

### Jump Mechanics
- **Coyote Time**: Allows the player to jump for a short time even after leaving the ground.
- **Variable Jump Height**: If the player releases the jump button early, the jump is cut short.
- **Jump Buffering**: The player can press jump slightly before landing, and the jump will still be executed.
- **Fall Speed Adjustment**: Gravity is increased after reaching the jump's peak for a smooth fall.
- **Prevents Quick Jump Spam**: Ensures only one jump occurs per key press.

## Usage
1. Attach the `SmoothCharacterControl` script to your character GameObject.
2. Assign a **Rigidbody2D** component to the GameObject.
3. Set up the **Floor** GameObjects with a `Floor` tag.
4. Assign a **Collider2D** to both your player GameObject and your "floor".
5. Ensure `Input.GetAxis("Horizontal")` and `Input.GetButton("Jump")` are mapped correctly in [Unity's Input Manager](https://docs.unity3d.com/Manual/class-InputManager.html).

The script automatically handles movement and jumping. Modify values in `MovementSettings` and `JumpSettings` to adjust behavior.

### Inspector Parameters
#### **MovementSettings**
| Parameter                 | Description                                      | Default |
|---------------------------|--------------------------------------------------|---------|
| `horizontalDamping`       | Controls the smoothness of movement transitions | `0.1f`  |
| `acceleration`            | Maximum speed of the character                  | `10f`   |

#### **JumpSettings**
| Parameter                            | Description                                                        | Default  |
|--------------------------------------|--------------------------------------------------------------------|----------|
| `CoyoteTime`                         | Time (in seconds) the player can jump after leaving the ground    | `0.15f`  |
| `MinJumpMultiplier`                   | How much the jump is reduced when releasing the button early      | `0.5f`   |
| `MinHeightToJumpShortClick`           | Minimum jump height when tapping the jump button                  | `5.0f`   |
| `SpeedToFallOnceReachedMaxHeight`     | Gravity multiplier applied when falling after peak jump height    | `2.5f`   |

## How It Works
### **Movement**
- Reads horizontal input (`Input.GetAxis("Horizontal")`).
- Uses **SmoothDamp** to interpolate movement for fluid acceleration/deceleration.
- Rotates the character based on direction it's going.

### **Jumping**
1. Checks if the jump button is pressed (`Input.GetButtonDown("Jump")`).
2. If within coyote time or on the ground, the character jumps.
3. If the jump button is released early, the jump height is reduced.
4. If falling, gravity is increased for a snappier descent.

## How to Modify
- Adjust `Jumpforce` in the script to change jump strength.
- Modify `MovementSettings.acceleration` to tweak movement speed.
- Tune `JumpSettings` to refine jump responsiveness and physics.

## Requirements
- Unity **2020+**

## Contributing
This project is open-source, and contributions are welcome! If you would like to improve or modify the script, feel free to:
1. Fork the repository.
2. Make your changes.
3. Submit a **Pull Request (PR)** with a description of your improvements.

All contributions are appreciated and reviewed. 🎉

---
### Author
Developed by **Gustavo Segato Gazzeta**.
