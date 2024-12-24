using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform

    public float orbitRadius = 1.8f; // Distance from the player

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - player.position;
        direction.z = 0; // Ensure we ignore the Z-axis

        // Calculate the angle in degrees and rotate the weapon
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270 + angle));

        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * orbitRadius;

        transform.position = player.position + offset;
    }
}