using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour
{
    public Shop shop;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shop.ConfirmBuy();
        }
    }
}
