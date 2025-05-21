using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float attackCooldown;
    public float attackDuration;
    public bool canAttack;
    public GameObject hitbox;

    public abstract IEnumerator Attack();
}
