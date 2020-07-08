using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Controls the player's movemet. Confined to horizontal movement
 * within the camera's view.
 */
public class PlayerMovement : MonoBehaviour
{
    /**
     * Player speed
     */
    [SerializeField]
    private float speed = 0.0f;

    /**
     * Player Rigidbody2D
     */
    private Rigidbody2D rb = null;

    /**
     * Half player width
     */
    private float halfWidth = 0.0f;

    /**
     * Initialize
     */
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null) {
            Debug.LogError("[PlayerMovement] No Rigidbody2D.");
            this.enabled = false;
        }
        halfWidth = GetComponent<BoxCollider2D>().bounds.size.x / 2.0f;
    }

    /**
     * Move ship within screen confines
     */
    private void FixedUpdate() {
        rb.MovePosition(new Vector2(ClampXPos(rb.position.x + (speed * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime)), rb.position.y));
    }

    /**
     * Clamps the given position to the screen constraints
     */
    private float ClampXPos(float xPos) {
        return Mathf.Clamp(xPos, ScreenUtils.ScreenLeft + halfWidth, ScreenUtils.ScreenRight - halfWidth);
    }
}
