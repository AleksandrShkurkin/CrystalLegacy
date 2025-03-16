using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wand Plasma", menuName = "Items/Weapons/Ranged/Wand Plasma")]
public class WandPlasma : WeaponRanged
{
    public float projectileSpeed = 10f;

    public override void FireProjectile(PlayerAttack player)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - player.transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab,
        player.transform.position + (Vector3)direction * 0.5f, Quaternion.identity);
        Player.Instance.Mana -= 5;
        if (Player.Instance.Mana < 0)
        {
            Player.Instance.Mana = 0;
        }

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        projectile.GetComponent<ProjectileDamage>().SetDamage(weaponDamage + Player.Instance.AttackDamage);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(projectile, 0.7f);
    }
}
