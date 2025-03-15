using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private int finalDamage;

    public void SetDamage(int dmg)
    {
        finalDamage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().RecieveDamage(finalDamage);
            Debug.Log("Attacked with " + finalDamage);
        }
    }
}
