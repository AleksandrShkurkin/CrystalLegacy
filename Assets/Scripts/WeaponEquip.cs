using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public GameObject weaponPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player"))
        {
            Transform weaponHolder = other.transform.Find("WeaponHolder");
            if (weaponHolder != null)
            {
                foreach (Transform child in weaponHolder)
                {
                    Destroy(child.gameObject);
                }

                GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder);
                newWeapon.transform.localPosition = Vector3.zero;
                newWeapon.transform.localRotation = Quaternion.identity;

                if (newWeapon.GetComponent<WeaponRange>() != null)
                {
                    weaponHolder.localPosition = Vector3.zero;
                }
                else
                {
                    weaponHolder.localPosition = new Vector3(0.4f, 0, 0);
                }
            }
        }
    }
}
