using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public Shop shop;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shop.AddToBasket(shop.itemsInStock[0]);
        }
    }
}
