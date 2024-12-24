using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;
    private float range;
    private Vector3 startPosition;

    public void Initialize(float bulletRange)
    {
        range = bulletRange;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
