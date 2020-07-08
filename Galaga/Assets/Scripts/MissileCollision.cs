using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Destroy object when hit by a missile
 */
public class MissileCollision : MonoBehaviour
{
    /**
     * Detect missiles
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Missile") && collision.GetComponent<Missile>().Target.Equals(tag)) {
            Destroy(gameObject);
        }
    }
}
