using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    private Animator animator;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    private int direction; // 0=Down, 1=Up, 2=Left, 3=Right

    void Start()
    {
        weaponName = "Sword";
        damage = 10;
        attackCooldown = 0.5f;
        attackDuration = 0.5f;
        canAttack = true;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        spriteRenderer.enabled = false;
        collider.enabled = false;
    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    public override IEnumerator Attack()
    {
        canAttack = false;

        // Позиціонування та поворот меча відносно гравця
        Vector3 offset = Vector3.zero;
        float rotationZ = 0f;

        switch (direction)
        {
            case 0: // вниз
                offset = new Vector3(0f, -0.5f, 0f);
                rotationZ = -90f;
                break;
            case 1: // вгору
                offset = new Vector3(0f, 0.5f, 0f);
                rotationZ = 90f;
                break;
            case 2: // вліво
                offset = new Vector3(-0.5f, 0f, 0f);
                rotationZ = 180f;
                break;
            case 3: // вправо
                offset = new Vector3(0.5f, 0f, 0f);
                rotationZ = 0f;
                break;
        }

        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        // Увімкнення меча
        spriteRenderer.enabled = true;
        collider.enabled = true;

        if (animator != null)
        {
            animator.Play("SwordAttack", 0, 0f); // Програти з початку
        }

        // Удар перший
        HashSet<Collider2D> hitSet = new HashSet<Collider2D>();
        Collider2D[] firstHits = Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in firstHits)
        {
            if (enemy.TryGetComponent(out Enemy e))
            {
                e.RecieveDamage(damage);
                hitSet.Add(enemy);
            }
        }

        yield return new WaitForSeconds(attackDuration / 2f);

        // Удар другий
        Collider2D[] secondHits = Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in secondHits)
        {
            if (!hitSet.Contains(enemy) && enemy.TryGetComponent(out Enemy e))
            {
                e.RecieveDamage(damage);
            }
        }

        yield return new WaitForSeconds(attackDuration / 2f);

        // Вимикаємо меч
        spriteRenderer.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
