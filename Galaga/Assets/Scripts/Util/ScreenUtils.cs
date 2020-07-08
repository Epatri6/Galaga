using UnityEngine;

/**
 * Provides data regarding the screen
 */
public static class ScreenUtils
{
    /**
     * Screen width
     */
    private static float screenWidth = 0.0f;

    /**
     * Screen Height
     */
    private static float screenHeight = 0.0f;

    /**
     * Borders of the screen
     */
    private static float screenTop, screenBottom, screenLeft, screenRight = 0.0f;

    /**
     * Getters
     */
    public static float ScreenTop { get { return screenTop; } }
    public static float ScreenBottom { get { return screenBottom; } }
    public static float ScreenLeft { get { return screenLeft; } }
    public static float ScreenRight { get { return screenRight; } }
    public static float ScreenWidth { get { return screenWidth; } }
    public static float ScreenHieght { get { return screenHeight; } }

    /**
     * Determine boundaries
     */
    public static void Initialize()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        screenTop = bounds.y;
        screenBottom = -bounds.y;
        screenRight = bounds.x;
        screenLeft = -bounds.x;
        screenWidth = screenRight - screenLeft;
        screenHeight = screenTop - screenBottom;
    }
}
