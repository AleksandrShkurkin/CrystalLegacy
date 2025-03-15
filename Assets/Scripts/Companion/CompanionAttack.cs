using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAttack : MonoBehaviour
{
    private Companion companion;
    private Enemy enemy;
    private float attackCooldown = 1.0f;
    private float nextAttackTime;

    void Start()
    {
        companion = GetComponentInParent<Companion>();
    }

    void Update()
    {
        if (enemy != null && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            AttackEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Enemy>() == enemy)
        {
            enemy = null;
        }
    }

    void AttackEnemy()
    {
        if (enemy != null)
        {
            enemy.RecieveDamage(companion.AttackDamage);
        }
    }
}
