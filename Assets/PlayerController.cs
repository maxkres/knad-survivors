using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator bodyAnimator;

    public float moveSpeed = 5;

    private float prevMoveX = 1;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Maps A/D keys (or Left/Right arrow keys)
        float moveY = Input.GetAxis("Vertical");   // Maps W/S keys (or Up/Down arrow keys)

        // Combine into a movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized;

        // Apply movement
        transform.Translate(movement * moveSpeed * Time.deltaTime);

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
