using UnityEngine;

public class HelpButtonFollowCamera : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Vector2 offset = new Vector2(10f, -10f); // Offset from top-right corner

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; // Auto-assign the main camera if not set

        // Get the camera's position in world space
        Vector3 cameraPosition = mainCamera.transform.position;

        // Convert camera world position to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(cameraPosition);

        // Apply offset to position the button relative to the screen's top-right corner
        Vector3 buttonPosition = new Vector3(
            Screen.width + offset.x,
            Screen.height + offset.y,
            screenPosition.z
        );

        // Convert back to world position for the UI button
        transform.position = mainCamera.ScreenToWorldPoint(buttonPosition);

        // Ensure the button stays on the same plane as the UI Canvas
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
