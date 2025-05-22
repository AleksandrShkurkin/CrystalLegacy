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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject visualsContainer;
    public float moveSpeed = 2f;
    private Vector2 movement;
    private Transform player;
    protected bool isAggressive = false;

    public float detectionRange = 5f;
    public float detectionDistance = 10f;
    public LayerMask obstacleMask;

    public Animator Animator => animator;

    private void Awake()
    {
        visualsContainer = new GameObject("VisualContainer");
        visualsContainer.transform.SetParent(transform);
        visualsContainer.transform.localPosition = Vector3.zero;
        visualsContainer.transform.localRotation = Quaternion.identity;
        visualsContainer.transform.localScale = Vector3.one;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            var newSpriteRenderer = visualsContainer.AddComponent<SpriteRenderer>();
            newSpriteRenderer.sprite = spriteRenderer.sprite;
            newSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
            Destroy(spriteRenderer);
            spriteRenderer = newSpriteRenderer;
        }
        
        var oldAnimator = GetComponent<Animator>();
        if (oldAnimator)
        {
            animator = visualsContainer.AddComponent<Animator>();
            animator.runtimeAnimatorController = oldAnimator.runtimeAnimatorController;
            Destroy(oldAnimator);
        }
    }

    private void Start()
    {
        rb = GetComponentInParent<Enemy>().GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player)
        {
            var direction = (player.position - transform.position).normalized;
            var distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= detectionDistance)
            {
                var hit = Physics2D.Raycast(transform.position, direction, detectionRange, obstacleMask);
                Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

                var forwardDirection = transform.right;
                var offset = transform.up * 0.5f;
                var hitLeft = Physics2D.Raycast(transform.position + offset, forwardDirection, 1.5f, obstacleMask);
                Debug.DrawRay(transform.position + offset, forwardDirection * 1.5f, Color.blue);

                var hitRight = Physics2D.Raycast(transform.position + (-offset), forwardDirection, 1.5f, obstacleMask);
                Debug.DrawRay(transform.position + (-offset), forwardDirection * 1.5f, Color.blue);

                if (!hit.collider && !hitLeft.collider && !hitRight.collider)
                {
                    movement = direction;
                    isAggressive = true;
                }
                else
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
        
        if (animator)
        {
            animator.SetBool("IsWalking", isAggressive);
        }
    }

    private void LateUpdate()
    {
        visualsContainer.transform.rotation = Quaternion.identity;
    }

    
    private void FixedUpdate()
    {
        if (!isAggressive) return;
        MoveCharacter(movement);
        RotateCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * (moveSpeed * Time.fixedDeltaTime)));
    }

    private void RotateCharacter(Vector2 movement)
    {
        if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, movement.x > 0 ? 0 : 180);
        }
        else
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, movement.y > 0 ? 90 : -90);
        }
        
        if (!animator) return;

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetInteger("Direction", movement.x > 0 ? 3 : 2);
        }
        else
        {
            animator.SetInteger("Direction", movement.y > 0 ? 1 : 0);
        }
    }

    private Vector2 FindAlternativePath(Vector2 originalDirection)
    {
        Vector2[] possibleDirections = {
            new Vector2(-originalDirection.y, originalDirection.x),
            new Vector2(originalDirection.y, -originalDirection.x),
            -originalDirection
        };

        foreach (var dir in possibleDirections)
        {
            if (Physics2D.Raycast(transform.position, dir, detectionRange, obstacleMask)) continue;
            Debug.DrawRay(transform.position, dir * detectionRange, Color.green);
            return dir;
        }

        return Vector2.zero;
    }
}