using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Rotation based on the mouse position
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }

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

        if (movement != Vector2.zero)
        {

        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Player.Instance.MoveSpeed * Time.fixedDeltaTime);
    }
}
