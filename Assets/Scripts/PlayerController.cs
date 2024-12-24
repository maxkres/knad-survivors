using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Animator bodyAnimator;

    private float prevMoveX = 1;

    public Tilemap tilemap;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletRange = 5f;

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized;

        transform.Translate(movement * PlayerStats.speed * Time.deltaTime);

        BoundsInt cellBounds = tilemap.cellBounds;
        Vector3 tileSize = tilemap.cellSize;
        Vector3 tileBounds = new Vector3(tileSize.x, tileSize.y, 0);
        minBounds = tilemap.CellToWorld(cellBounds.min) + tileBounds / 2;
        maxBounds = tilemap.CellToWorld(cellBounds.max) + new Vector3(-tileSize.x / 2, 0, 0);

        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z
        );

        transform.position = clampedPosition;

        if (!movement.Equals(Vector3.zero))
        {
            bodyAnimator.ResetTrigger("idle");
            bodyAnimator.SetTrigger("walk");
        }
        else
        {
            bodyAnimator.ResetTrigger("walk");
            bodyAnimator.SetTrigger("idle");
        }

        if (moveX != 0 && moveX * prevMoveX < 0)
        {
            foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.flipX = !sprite.flipX;
            }
            prevMoveX = moveX;
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "Bullets";
            spriteRenderer.sortingOrder = 1; 
        }

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(bulletRange);
        }
    }
}
