using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecourceZone : MonoBehaviour
{
    public MaterialType materialType;
    private CooldownManager cooldownManager = new CooldownManager(3f);
    private Coroutine collectionCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectionCoroutine == null)
            {
                collectionCoroutine = StartCoroutine(CollectResources());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectionCoroutine != null)
            {
                StopCoroutine(collectionCoroutine);
                collectionCoroutine = null;
            }
        }
    }

    private IEnumerator CollectResources()
    {
        while (true)
        {
            if (cooldownManager.IsCooldownFinished(Time.time))
            {
                cooldownManager.InitiateCooldown(Time.time);
                Inventory.Instance.AddMaterial(materialType, 1);
                Debug.Log("Added " + materialType.ToString() + ": 1, Total: " + Inventory.Instance.GetMaterialAmount(materialType));
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
