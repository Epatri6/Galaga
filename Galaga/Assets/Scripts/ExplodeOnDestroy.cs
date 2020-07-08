using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Plays an explosion when the attached object is destroyed
 */
public class ExplodeOnDestroy : MonoBehaviour
{
    /**
     * Explosion effect
     */
    [SerializeField]
    private GameObject effect = null;

    /**
     * Play effect when destroyed
     */
    private void OnDestroy() {
        Instantiate(effect, transform.position, effect.transform.rotation);
    }
}
