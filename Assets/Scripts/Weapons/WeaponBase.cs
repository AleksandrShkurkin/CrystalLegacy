using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;
    public int damageWeapon;

    public abstract IEnumerator Attack(int dmg);
}
