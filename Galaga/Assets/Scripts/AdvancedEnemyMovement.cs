using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Enemy moves to bottom of screen, then loop-de-loops
 */
public class AdvancedEnemyMovement : MonoBehaviour
{

    /**
     * How quickly to rotate
     */
    [SerializeField]
    private float rotationFactor = 1.0f;

    /**
     * Speed of loop
     */
    [SerializeField]
    private float loopSpeed = 45.0f;

    /**
     * How much we've looped
     */
    private float currLoop = 0.0f;

    /**
     * Rigidbody
     */
    private Rigidbody2D rb = null;

    /**
     * Enemy data
     */
    private Enemy data = null;

    /**
     * Represents which type of movement we're doing
     */
    private enum MODE {
        MOVE_BOTTOM, LOOP
    }

    private MODE mode = MODE.MOVE_BOTTOM;

    /**
     * Movement sound
     */
    [SerializeField]
    private AudioSource sound = null;

    /**
     * Setup
     */
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        data = GetComponent<Enemy>();
        data.AddStateChangedListener(PlaySound);
    }

    /**
     * Plays sound
     */
     public void PlaySound() {
        if(data.ActionState != Enemy.Action_State.ATTACKING) {
            return;
        }
        sound.Play();
    }

    /**
     * Move to bottom of screen
     */
    private void MoveToBottom() {
        rb.rotation = 270.0f + (Mathf.Sin(Time.time * rotationFactor) * 60.0f);
        rb.velocity = data.Speed * rb.transform.right;
        if(rb.transform.position.y < GameObject.FindGameObjectWithTag("Player").transform.position.y) {
            if(rb.transform.rotation.eulerAngles.z > 270) {
                rb.angularVelocity = loopSpeed;
            }
            else {
                rb.angularVelocity = -loopSpeed;
            }
            mode = MODE.LOOP;
            currLoop = 0.0f;
        }
    }

    /**
     * Do a loop de loop
     */
     private void Loop() {
        currLoop += loopSpeed * Time.fixedDeltaTime;
        if(currLoop > 360.0f) {
            if(270.0f - transform.rotation.eulerAngles.z <= 10.0f) {
                rb.angularVelocity = 0.0f;
            }
            else if(270.0f - transform.rotation.eulerAngles.z > 0) {
                rb.angularVelocity = loopSpeed;
            }
            else {
                rb.angularVelocity = -loopSpeed;
            }
        }
        rb.velocity = data.Speed * rb.transform.right;
        if(transform.position.y > ScreenUtils.ScreenTop) {
            rb.angularVelocity = 0.0f;
            mode = MODE.MOVE_BOTTOM;
        }
    }

    /**
     * Move
     */
    private void FixedUpdate() {
        if(data.ActionState != Enemy.Action_State.ATTACKING) {
            return;
        }
        switch(mode) {
            case MODE.MOVE_BOTTOM:
                MoveToBottom();
                break;
            case MODE.LOOP:
                Loop();
                break;
        }
        if(transform.position.y + 0.5f < ScreenUtils.ScreenBottom) {
            transform.position = new Vector2(transform.position.x, ScreenUtils.ScreenTop + (0.2f * ScreenUtils.ScreenHieght));
        }
        if(Mathf.Abs(transform.position.x) - 0.5f > ScreenUtils.ScreenRight) {
            transform.position = new Vector2(Mathf.Clamp(-transform.position.x, ScreenUtils.ScreenLeft - 0.4f, ScreenUtils.ScreenRight + 0.4f), transform.position.y);
        }
    }

   /**
    * Remove listener when destroyed
    */
    private void OnDestroy() {
        data.DeleteStateChangedListener(PlaySound);
    }
}
