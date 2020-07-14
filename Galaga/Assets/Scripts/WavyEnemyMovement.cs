using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Moves the enemy in a wavy pattern
 */
public class WavyEnemyMovement : MonoBehaviour
{
    /**
     * Enemy Rigidbody2d
     */
    private Rigidbody2D rb = null;

    /**
     * How fast it curves
     */
    [SerializeField]
    private float rotationFactor = 1.0f;

    /**
     * Enemy data
     */
    private Enemy data = null;

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
     * Rotate and move
     */
    private void FixedUpdate() {

        //must be attacking
        if(data.ActionState != Enemy.Action_State.ATTACKING) {
            return;
        }

        //Calculate wave pattern
        rb.rotation = 270.0f + (Mathf.Sin(Time.time * rotationFactor) * 60.0f);
        rb.velocity = data.Speed * rb.transform.right;

        //Wrap when going off screen
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
