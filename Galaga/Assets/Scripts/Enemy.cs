using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
/**
 * Model for enemy units
 */
public class Enemy : MonoBehaviour {
    /**
     * Enum representing action state
     */
    public enum Action_State {
        IDLE, ATTACKING, RETURNING, SPAWNING
    }

    /**
     * Enemy speed
     */
    [SerializeField]
    private float speed = 0.0f;

    /**
     * Enemy firing cooldown
     */
    [SerializeField]
    private float firingCooldown = 0.0f;

    /**
     * Enemy action state
     */
    private Action_State actionState = Action_State.IDLE;

    //-----------------------Getters/Setters---------------------------
    public float Speed { get { return speed; } set { speed = value; } }
    public float FiringCooldown { get { return firingCooldown; } }
    public Action_State ActionState {
        get { return actionState; }
        set { actionState = value; actionStateChanged.Invoke(); }
    }

    //-------------------------Events----------------------------------

    /**
     * Event for action state changes
     */
    private UnityEvent actionStateChanged = new UnityEvent();
    public void AddStateChangedListener(UnityAction action) { actionStateChanged.AddListener(action); }
    public void DeleteStateChangedListener(UnityAction action) { actionStateChanged.RemoveListener(action); }

    private void Update() {
        if(Time.time > 5.0f) {
            ActionState = Action_State.ATTACKING;
        }
    }
}
