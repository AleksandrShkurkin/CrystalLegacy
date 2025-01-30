using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 4f;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            transform.rotation = Quaternion.Euler(0, 0, direction.x > 0 ? 0 : 180);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, direction.y > 0 ? 90 : -90);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
