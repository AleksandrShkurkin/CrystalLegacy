// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyMoveFollow : MonoBehaviour
// {
//     private Rigidbody2D rb;
//     public float moveSpeed = 2f;
//     private Vector2 movement;
//     private Transform player;
//     protected bool isAggresive = false;

//     void Start()
//     {
//         rb = GetComponentInParent<Enemy>().GetComponent<Rigidbody2D>();
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     void Update()
//     {
//         if (player != null)
//         {
//             Vector3 direction = player.position - transform.position;
//             direction.Normalize();
//             movement = direction;
//         }
//     }

//     void FixedUpdate()
//     {
//         if (isAggresive)
//         {
//             MoveCharacter(movement);
//             RotateCharacter(movement);
//         }
//     }

//     void MoveCharacter(Vector2 direction)
//     {
//         rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
//     }

//     void RotateCharacter(Vector2 movement)
//     {
//         if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
//         {
//             rb.transform.rotation = Quaternion.Euler(0, 0, movement.x > 0 ? 0 : 180);
//         }
//         else
//         {
//             rb.transform.rotation = Quaternion.Euler(0, 0, movement.y > 0 ? 90 : -90);
//         }
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             isAggresive = true;
//         }
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             isAggresive = false;
//         }
//     }
// }

//second attempt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2f;
    private Vector2 movement;
    private Transform player;
    protected bool isAggressive = false;

    public float detectionRange = 5f; // Raycast range
    public float detectionDistance = 10f; // Distance to detect player
    public LayerMask obstacleMask; // Assign "Obstacle" layer in Inspector

    void Start()
    {
        rb = GetComponentInParent<Enemy>().GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= detectionDistance)
            {
                // Check for obstacles and player detection
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, obstacleMask);
                Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

                if (hit.collider == null) // No obstacle, move directly to player
                {
                    movement = direction;
                    isAggressive = true;
                }
                else // Obstacle detected, find a different path
                {
                    movement = FindAlternativePath(direction);
                    isAggressive = movement != Vector2.zero;
                }
            }
            else
            {
                isAggressive = false;
            }
        }
        else
        {
            isAggressive = false;
        }
    }

    void FixedUpdate()
    {
        if (isAggressive)
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

    Vector2 FindAlternativePath(Vector2 originalDirection)
    {
        Vector2[] possibleDirections = {
            new Vector2(-originalDirection.y, originalDirection.x), // Left
            new Vector2(originalDirection.y, -originalDirection.x), // Right
            -originalDirection // Backward
        };

        foreach (Vector2 dir in possibleDirections)
        {
            if (!Physics2D.Raycast(transform.position, dir, detectionRange, obstacleMask))
            {
                Debug.DrawRay(transform.position, dir * detectionRange, Color.green);
                return dir;
            }
        }

        return Vector2.zero; // No available paths, stop moving
    }
}