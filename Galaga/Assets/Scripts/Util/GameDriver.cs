using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
/**
 * Loads various game components at start. Attach to scene main camera.
 */
public class GameDriver : MonoBehaviour
{
    /**
     * Startup Initialization
     */
    void Start()
    {
        ScreenUtils.Initialize();
    }

}
