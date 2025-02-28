using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWand2 : WeaponRange
{
    public float attackDuration = 4f;
    public float maxAttackDistance = 7f;
    void Start()
    {
        weaponName = "Wand Meteors";
        damageWeapon = 25;
    }

    public override IEnumerator Attack(int playerDamage)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Vector3.Distance(transform.position, mousePos) > maxAttackDistance)
        {
            yield break;
        }

        GameObject fireball = Instantiate(projectilePrefab, mousePos, Quaternion.identity);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fireball.transform.position, fireball.GetComponent<CircleCollider2D>().radius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().RecieveDamage(damageWeapon + playerDamage);
        }

        yield return new WaitForSeconds(1f);

        Destroy(fireball);

        yield return new WaitForSeconds(attackDuration);
    }
}
