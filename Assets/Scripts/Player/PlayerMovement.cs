using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 4f;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Зберігає останній напрямок руху (0=Down, 1=Up, 2=Side)
    private int lastDirection = 0;
    private bool lastFlipX = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Отримуємо ввід гравця
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Нормалізація діагонального руху
        if (movement.sqrMagnitude > 1)
            movement = movement.normalized;

        bool isWalking = movement != Vector2.zero;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            // Напрямок — вбік
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                animator.SetInteger("Direction", 2); // Side
                spriteRenderer.flipX = movement.x < 0;
                lastDirection = 2;
                lastFlipX = spriteRenderer.flipX;
            }
            else if (movement.y > 0)
            {
                animator.SetInteger("Direction", 1); // Up
                lastDirection = 1;
            }
            else
            {
                animator.SetInteger("Direction", 0); // Down
                lastDirection = 0;
            }
        }
        else
        {
            // Коли стоїть — зберігаємо останній напрямок і фліп
            animator.SetInteger("Direction", lastDirection);
            if (lastDirection == 2)
                spriteRenderer.flipX = lastFlipX;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}


// Original code
// void Update()
// {
//     movement = Vector2.zero;
//     movement.x = Input.GetAxisRaw("Horizontal");
//     movement.y = Input.GetAxisRaw("Vertical");

//     if (movement != Vector2.zero)
//     {
//         if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
//         {
//             transform.rotation = Quaternion.Euler(0, 0, movement.x > 0 ? 0 : 180);
//         }
//         else
//         {
//             transform.rotation = Quaternion.Euler(0, 0, movement.y > 0 ? 90 : -90);
//         }
//     }
// }

// Rotation based on the mouse position