using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponSword weapon;

    void Start()
    {
        weapon = GetComponentInChildren<WeaponSword>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weapon.canAttack)
        {
            StartCoroutine(weapon.Attack());
        }
    }
}
