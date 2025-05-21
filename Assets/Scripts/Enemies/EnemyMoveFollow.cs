using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float moveSpeed = 2f;

    private Vector2 movement;
    private Transform player;
    protected bool isAggresive = false;

    void Start()
    {
        rb = GetComponentInParent<Enemy>().GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;

            if (isAggresive)
            {
                RotateCharacter(movement);
            }
        }

        if (animator != null)
        {
            animator.SetBool("IsWalking", isAggresive);
        }
    }

    void FixedUpdate()
    {
        if (isAggresive)
        {
            MoveCharacter(movement);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    void RotateCharacter(Vector2 movement)
    {
        if (animator == null) return;

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetInteger("Direction", movement.x > 0 ? 3 : 2); // 3 = Right, 2 = Left
        }
        else
        {
            animator.SetInteger("Direction", movement.y > 0 ? 1 : 0); // 1 = Up, 0 = Down
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAggresive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAggresive = false;
        }
    }
}
