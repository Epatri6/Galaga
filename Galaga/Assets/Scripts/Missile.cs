using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Missiles collide with targets and explode
 */
public class Missile : MonoBehaviour
{
    /**
     * Missile explosion effect
     */
    [SerializeField]
    private GameObject explosion = null;

    /**
     * Speed
     */
    [SerializeField]
    private float speed = 0.0f;

    /**
     * Tag of target
     */
    private string target = "";
    public string Target { get { return target; } }

    /**
     * Called by creators to initialize missile
     */
    public void Initialize(string target, Vector2 dir) {
        this.target = target;
        GetComponent<Rigidbody2D>().velocity = speed * dir;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag(target)) {
            Instantiate(explosion, collision.ClosestPoint(transform.position), explosion.transform.rotation);
            Destroy(gameObject);
        }
    }

    /**
     * Destroy when offscreen
     */
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
