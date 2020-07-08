using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Allows the player to shoot on key press
 */
public class PlayerShooting : MonoBehaviour
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
     * Is key pressed?
     */
    private bool pressed = false;

    /**
     * Fire on key press
     */
    private void Update() {
        if(!pressed && Input.GetAxisRaw("Shoot") > 0.0f) {
            Instantiate(missile, transform.position, missile.transform.rotation).GetComponent<Missile>().Initialize("Enemy", Vector2.up);
            sound.Play();
            pressed = true;
        }
        else if(pressed && Input.GetAxisRaw("Shoot") == 0.0f) {
            pressed = false;
        }
    }
}
