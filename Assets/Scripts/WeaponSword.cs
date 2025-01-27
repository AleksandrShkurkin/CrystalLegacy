using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    void Start()
    {
        weaponName = "Sword";
        damage = 10;
        attackCooldown = 0.5f;
        attackDuration = 0.5f;
        canAttack = true;
        hitbox = gameObject;
    }

    public override IEnumerator Attack()
    {
        canAttack = false;
        hitbox.GetComponent<Collider2D>().enabled = true;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("Enemy"));
        Debug.Log(hitEnemies.Length);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().RecieveDamage(damage);
        }

        yield return new WaitForSeconds(attackDuration);

        hitbox.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
