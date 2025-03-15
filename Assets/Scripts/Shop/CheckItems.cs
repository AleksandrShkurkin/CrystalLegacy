using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItems : MonoBehaviour
{
    public Shop shop;
    private List<ShopItem> itemsInBasket;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            itemsInBasket = shop.itemBasket;
            foreach (ShopItem item in itemsInBasket)
            {
                Debug.Log(item.item.name + " " + item.amount);
            }
        }
    }
}
