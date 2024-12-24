using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public Transform player;

    public float orbitRadius = 1.8f;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - player.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270 + angle));

        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * orbitRadius;

        transform.position = player.position + offset;
    }
}