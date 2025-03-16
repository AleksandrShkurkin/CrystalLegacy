using UnityEngine;

public enum WeaponType { Melee, Ranged }

public abstract class Weapon : Item
{
    public WeaponType weaponType;
    public int weaponDamage;

    public abstract void Attack(PlayerAttack player);
}
