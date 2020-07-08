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
     * Fire missile script
     */
    private EnemyFireMissile missile = null;

    /**
     * Max chance to fire missile
     */
    private float maxChance = 0.5f;

    /**
     * Min chance to fire missile
     */
    private float minChance = 0.1f;

    /**
     * Current chance
     */
    private float currChance = 0.5f;

    /**
     * Time between shots
     */
    private float cooldown = 1.0f;

    /**
     * Time of last shot
     */
    private float timeLastShot = 0.0f;

    /**
     * Enemy speed
     */
    [SerializeField]
    private float speed = 0.0f;

    /**
     * How fast it curves
     */
    [SerializeField]
    private float rotationFactor = 1.0f;

    /**
     * Sound for moving
     */
    [SerializeField]
    private AudioSource sound = null;

    /**
     * Setup
     */
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        missile = GetComponent<EnemyFireMissile>();
    }

    private void Awake() {
        sound.Play();
    }

    /**
     * Rotate and move
     */
    private void FixedUpdate() {
        rb.rotation = 270.0f + (Mathf.Sin(Time.time * rotationFactor) * 60.0f);
        rb.velocity = speed * rb.transform.right;
        if(Time.time - timeLastShot > cooldown) {
            float newChance = 0.0f;
            if(Random.Range(0.0f, 1.0f) < currChance) {
                missile.Fire();
                newChance = currChance / 3.0f;
            }
            else {
                newChance = currChance * 2.0f;
            }
            currChance = Mathf.Clamp(newChance, minChance, maxChance);
            timeLastShot = Time.time;
        }
        if(transform.position.y + 0.5f < ScreenUtils.ScreenBottom) {
            transform.position = new Vector2(transform.position.x, ScreenUtils.ScreenTop + (0.2f * ScreenUtils.ScreenHieght));
        }
        if(Mathf.Abs(transform.position.x) - 0.5f > ScreenUtils.ScreenRight) {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
    }
}
