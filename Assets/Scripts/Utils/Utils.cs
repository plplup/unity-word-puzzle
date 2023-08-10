using UnityEngine;

public static class Utils 
{
    public static float GetHorizontalExtensionFromViewBounds(Camera camera)
    {
        return (camera.orthographicSize * 2) * Screen.width / Screen.height;
    }
}
