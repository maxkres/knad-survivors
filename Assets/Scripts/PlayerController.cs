using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Animator bodyAnimator;

    public float moveSpeed = 5;

    private float prevMoveX = 1;

    public Tilemap tilemap;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Maps A/D keys (or Left/Right arrow keys)
        float moveY = Input.GetAxis("Vertical");   // Maps W/S keys (or Up/Down arrow keys)

        // Combine into a movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized;

        // Apply movement
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        BoundsInt cellBounds = tilemap.cellBounds;
        Vector3 tileSize = tilemap.cellSize;
        Vector3 tileBounds = new Vector3(tileSize.x, tileSize.y, 0);
        minBounds = tilemap.CellToWorld(cellBounds.min) + tileBounds / 2;
        maxBounds = tilemap.CellToWorld(cellBounds.max) + new Vector3(- tileSize.x / 2, 0, 0);

        // Clamp the player's position within the Tilemap bounds
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z // Keep Z-axis unchanged for 2D
        );

        transform.position = clampedPosition;

        // Animation
        if (!movement.Equals(Vector3.zero)) {
            bodyAnimator.ResetTrigger("idle");
            bodyAnimator.SetTrigger("walk");
        }
        else {
            bodyAnimator.ResetTrigger("walk");
            bodyAnimator.SetTrigger("idle");
        }

        if (moveX != 0 && moveX * prevMoveX < 0) {
            foreach (var sprite in GetComponentsInChildren<SpriteRenderer>()) {
                sprite.flipX = !sprite.flipX;
            }
            prevMoveX = moveX;
        }
    }
}
