using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private Shop currentShop;
    public GameObject shopUI;
    private bool inRange = false;

    private void Start()
    {
        currentShop = GetComponent<Shop>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.ToggleMenu(shopUI);
            if (shopUI.activeSelf)
            {
                UIShop.Instance.OpenShop(currentShop);
            }
            else
            {
                UIShop.Instance.CloseShop();
            }
        }
    }
}
