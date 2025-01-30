using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2f;
    private Vector2 movement;
    private Transform player;
    public GameObject aggresionTrigger;
    private Collider2D aggresionCollider;
    private bool isAggresive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aggresionCollider = aggresionTrigger.GetComponent<Collider2D>();
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
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
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
