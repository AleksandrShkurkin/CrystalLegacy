using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecourceZone : MonoBehaviour
{
    public MaterialType materialType;
    private CooldownManager cooldownManager = new CooldownManager(3f);
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!cooldownManager.IsCooldownFinished(Time.time))
            {
                return;
            }
            cooldownManager.InitiateCooldown(Time.time);
            Inventory.Instance.AddMaterial(materialType, 1);
            Debug.Log("Added " + materialType.ToString() + ": 1, Total: " + Inventory.Instance.GetMaterialAmount(materialType));
        }
    }
}
