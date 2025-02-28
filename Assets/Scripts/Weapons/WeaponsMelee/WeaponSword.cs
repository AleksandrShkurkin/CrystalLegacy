using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : WeaponMelee
{
    void Start()
    {
        weaponName = "Sword";
        damageWeapon = 10;
        colliderActiveTime = 0.5f;
        hitbox = gameObject;
    }

    public override IEnumerator Attack(int playerDamage)
    {
        hitbox.GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(colliderActiveTime/2);

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().RecieveDamage(damageWeapon + playerDamage);
        }

        yield return new WaitForSeconds(colliderActiveTime/2);

        hitbox.GetComponent<Collider2D>().enabled = false;
    }

    /* Damage at the start and the end of the attack
    Collider2D[] hitEnemiesFirstCheck = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("Enemy"));
        HashSet<Collider2D> hitEnemiesSet = new HashSet<Collider2D>(hitEnemiesFirstCheck);

        foreach (Collider2D enemy in hitEnemiesFirstCheck)
        {
            enemy.GetComponent<Enemy>().RecieveDamage(damage);
        }

        yield return new WaitForSeconds(attackDuration);

        hitbox.GetComponent<Collider2D>().enabled = false;

        Collider2D[] hitEnemiesSecondCheck = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.GetComponent<BoxCollider2D>().size, 0f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemiesSecondCheck)
        {
            if (!hitEnemiesSet.Contains(enemy))
            {
                enemy.GetComponent<Enemy>().RecieveDamage(damage);
            }
        }
    */
}
