using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponSword weapon;

    void Start()
    {
        // Sword Ч окремий обТЇкт у сцен≥
        weapon = FindObjectOfType<WeaponSword>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weapon != null && weapon.canAttack)
        {
            int direction = GetComponent<Animator>().GetInteger("Direction");
            weapon.SetDirection(direction); // новий р€док
            StartCoroutine(weapon.Attack());
        }
    }

}
