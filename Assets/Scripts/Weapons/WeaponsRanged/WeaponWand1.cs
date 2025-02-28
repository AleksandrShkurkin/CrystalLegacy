using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWand1 : WeaponRange
{
    public float bulletSpeed = 10f;
    void Start()
    {
        weaponName = "Wand Bullets";
        damageWeapon = 5;
    }

    public override IEnumerator Attack(int playerDamage)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - transform.position).normalized;
        Vector3 spawnPosition = transform.position + (Vector3)direction * 0.5f;

        GameObject bullet = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        bullet.GetComponent<BulletDamage>().SetDamage(damageWeapon + playerDamage);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(bullet, 0.7f);

        yield return new WaitForSeconds(0);
    }
}
