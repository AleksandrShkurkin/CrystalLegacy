using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponBase weapon;
    public Player player;
    public GameObject weaponHolder;
    public float attackCooldown = 0.5f;
    public bool canAttack = true;

    void Start()
    {
        weapon = weaponHolder.GetComponentInChildren<WeaponBase>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        weapon = weaponHolder.GetComponentInChildren<WeaponBase>();

        int finalDMG = player.attackDamage;
        if (Random.value < 0.15f)
        {
            finalDMG = Mathf.RoundToInt(player.attackDamage * 1.5f);
        }

        yield return StartCoroutine(weapon.Attack(finalDMG));
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
