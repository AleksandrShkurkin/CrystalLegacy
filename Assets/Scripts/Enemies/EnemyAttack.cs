using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    private Animator animator;
    private bool playerInRange;
    private float attackCooldown = 1.0f;
    private bool canAttack = true;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (playerInRange && canAttack)
        {
            StartCoroutine(AttackSequence());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    IEnumerator AttackSequence()
    {
        canAttack = false;

        // Встановлюємо напрямок
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                animator.SetInteger("Direction", direction.x > 0 ? 3 : 2); // right : left
            else
                animator.SetInteger("Direction", direction.y > 0 ? 1 : 0); // up : down

            animator.SetTrigger("AttackTrigger");

            // наносимо урон одразу після запуску анімації
            player.GetComponent<Player>().RecieveDamage(enemy.attackDamage);
        }

        // Чекаємо завершення атаки (припустимо 0.5 секунд — налаштуй під довжину твоєї анімації)
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
