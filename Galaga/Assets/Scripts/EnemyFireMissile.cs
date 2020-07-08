using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Fires a missile at the player
 */
public class EnemyFireMissile : MonoBehaviour
{
    /**
     * Missile prefab
     */
    [SerializeField]
    private GameObject missile = null;

    /**
     * Shooting audio
     */
    [SerializeField]
    private AudioSource sound = null;

    /**
     * Threshold distance from player at which we don't allow shooting
     */
    private const float threshold = 0.5f;

    /**
     * Min angle of a shot
     */
    private const float minAngle = 260.0f;

    /**
     * Max angle of a shot
     */
    private const float maxAngle = 280.0f;

    float timelastshot = 0.0f;
    private void Update() {
        if(Time.time - timelastshot > 1.0f) {
            Fire();
            timelastshot = Time.time;
        }
    }

    /**
     * Fires missile
     */
    public void Fire() {

        //don't fire when close to player
        if(transform.position.y - ScreenUtils.ScreenBottom < ScreenUtils.ScreenHieght * threshold) {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = (player.transform.position - transform.position).normalized;

        //Check against max angle
        float angle = Mathf.Atan2(dir.y, dir.x);
        angle = (angle > 0 ? angle : (2 * Mathf.PI + angle)) * 360 / (2 * Mathf.PI);
        angle = Mathf.Clamp(angle, minAngle, maxAngle) * Mathf.Deg2Rad;

        Missile mis = Instantiate(missile, transform.position, missile.transform.rotation).GetComponent<Missile>();
        mis.Initialize("Player", new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
        sound.Play();
    }
}
