using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private EnemyMoveFollow enemyMove;
    public bool playerInRange;
    private float attackCooldown = 1.0f;
    private float nextAttackTime;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyMove = transform.parent.GetComponent<EnemyMoveFollow>();
        if (enemyMove)
        {
            animator = enemyMove.Animator;
        }
    }

    private void Update()
    {
        if (!playerInRange || !(Time.time >= nextAttackTime)) return;
        nextAttackTime = Time.time + attackCooldown;
        AttackPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void AttackPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;
        
        Debug.Log("Attacking");
        
        Vector2 direction = (player.transform.position - transform.position).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            animator.SetInteger("Direction", direction.x > 0 ? 3 : 2);
        else
            animator.SetInteger("Direction", direction.y > 0 ? 1 : 0);

        animator.SetTrigger("AttackTrigger");
        player.GetComponent<Player.Player>().RecieveDamage(enemy.AttackDamage);
    }
}
