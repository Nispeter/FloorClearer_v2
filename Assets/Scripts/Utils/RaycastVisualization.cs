using UnityEngine;

public static class RaycastVisualization
{
    public static Color rayColor = Color.red;
    public static float rayDuration = 1.0f;

    // Visualize a raycast in the scene view
    public static void VisualizeRaycast(Vector3 origin, Vector3 direction, float distance)
    {
        Debug.DrawRay(origin, direction * distance, rayColor, rayDuration);
    }
}
