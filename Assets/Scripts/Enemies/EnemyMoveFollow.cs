using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2f;
    private Vector2 movement;
    private Transform player;
    protected bool isAggresive = false;

    void Start()
    {
        rb = GetComponentInParent<Enemy>().GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }
    }

    void FixedUpdate()
    {
        if (isAggresive)
        {
            MoveCharacter(movement);
            RotateCharacter(movement);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    void RotateCharacter(Vector2 movement)
    {
        if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, movement.x > 0 ? 0 : 180);
        }
        else
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, movement.y > 0 ? 90 : -90);
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
