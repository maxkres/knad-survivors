using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Vector3 offset;   // Optional offset for camera position

    void LateUpdate()
    {
        if (player != null)
        {
            // Keep the camera's current Z position while following the player's X and Y
            transform.position = new Vector3(
                player.position.x + offset.x, 
                player.position.y + offset.y, 
                transform.position.z // Keep the Z position unchanged
            );
        }
    }
}
