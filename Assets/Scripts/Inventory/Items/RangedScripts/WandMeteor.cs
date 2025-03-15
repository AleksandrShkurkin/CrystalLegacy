using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wand Meteor", menuName = "Items/Weapons/Ranged/Wand Meteor")]
public class WandMeteor : WeaponRanged
{
    public override void FireProjectile(PlayerAttack player)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        GameObject fireball = Instantiate(projectilePrefab, mousePos, Quaternion.identity);
        Player.Instance.Mana -= 15;
        if (Player.Instance.Mana < 0)
        {
            Player.Instance.Mana = 0;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fireball.transform.position, fireball.GetComponent<CircleCollider2D>().radius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().RecieveDamage(weaponDamage + Player.Instance.AttackDamage);
        }

        Destroy(fireball, 0.8f);
    }
}
