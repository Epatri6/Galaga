using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Controls idling
 */
public class EnemyIdle : MonoBehaviour
{
    /**
     * Enemy data
     */
    private Enemy data = null;

    /**
     * Rigidbody2d
     */
    private Rigidbody2D rb = null;

    /**
     * Horizontal distance
     */
    [SerializeField]
    private float offset = 0.0f;

    /**
     * Starting spot of idle
     */
    private float startPos = 0.0f;

    /**
     * Setup
     */
    private void Start() {
        data = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        data.AddStateChangedListener(SetStart);
        startPos = transform.position.x;
    }

    /**
     * Set start position
     */
     private void SetStart() {
        if(data.ActionState == Enemy.Action_State.IDLE) {
            startPos = rb.transform.position.x;
        }
    }

    /**
     * Float back and forth
     */
    private void FixedUpdate() {
        if(data.ActionState != Enemy.Action_State.IDLE) {
            return;
        }
        rb.MovePosition(new Vector2(startPos - offset + (Mathf.PingPong(Time.time, offset * 2)), rb.transform.position.y));
    }

    /**
     * Remove listener on death
     */
    private void OnDestroy() {
        data.DeleteStateChangedListener(SetStart);
    }
}
