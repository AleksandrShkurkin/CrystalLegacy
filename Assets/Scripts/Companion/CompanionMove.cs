using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2f;
    private Vector2 movement;
    private Transform player;
    private Transform enemy;
    public float followDistance = 3f;
    public float detectionDistance = 9f;
    public float teleportDistance = 15f;
    public LayerMask enemyMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer > teleportDistance)
            {
                transform.position = player.position + (Vector3)(Random.insideUnitCircle.normalized * followDistance);
            }
            else
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionDistance, enemyMask);
                if (enemies.Length > 0)
                {
                    enemy = GetClosestEnemy(enemies);
                    Vector3 direction = (enemy.position - transform.position).normalized;
                    movement = direction;
                }
                else
                {
                    if (distanceToPlayer > followDistance)
                    {
                        Vector3 direction = (player.position - transform.position).normalized;
                        movement = direction;
                    }
                    else
                    {
                        movement = Vector2.zero;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
        RotateCharacter(movement);
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

    Transform GetClosestEnemy(Collider2D[] enemies)
    {
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyCollider.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemyCollider.transform;
                minDistance = distance;
            }
        }

        return closestEnemy;
    }
}