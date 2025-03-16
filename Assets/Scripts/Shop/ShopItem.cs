using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public Item item;
    public int price;
    public int amount;

    public ShopItem(Item item, int price, int amount)
    {
        this.item = item;
        this.price = price;
        this.amount = amount;
    }
}
