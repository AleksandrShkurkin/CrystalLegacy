using UnityEngine;

public abstract class WeaponRanged : Weapon
{
    public GameObject projectilePrefab;
    
    public override void Attack(PlayerAttack player)
    {
        FireProjectile(player);
    }

    public abstract void FireProjectile(PlayerAttack player);
}
