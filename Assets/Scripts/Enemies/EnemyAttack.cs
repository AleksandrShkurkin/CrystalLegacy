using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    private bool playerInRange;
    private float attackCooldown = 1.0f;
    private float nextAttackTime;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void Update()
    {
        if (playerInRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            AttackPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void AttackPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.CompareTag("Player"))
        {
            player.GetComponent<Player>().RecieveDamage(enemy.attackDamage);
        }
    }
}
