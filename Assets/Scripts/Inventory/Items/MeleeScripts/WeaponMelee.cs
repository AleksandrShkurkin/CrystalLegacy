using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Items/Weapons/Melee" )]
public class WeaponMelee : Weapon
{
    public override void Attack(PlayerAttack player)
    {
        player.ActivateHitbox(weaponDamage);
    }
}
