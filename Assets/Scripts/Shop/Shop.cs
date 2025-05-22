using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopItem> itemsInStock = new List<ShopItem>();
    public List<ShopItem> itemBasket = new List<ShopItem>();
    public int total = 0;

    public void Start()
    {
        foreach (ShopItem item in itemsInStock)
        {
            item.amount = Random.Range(0, 15);
            ShopItem newItem = new ShopItem(item.item, item.price, 0);
            itemBasket.Add(newItem);
        }
    }

    public void AddToBasket(ShopItem item)
    {
        if (itemsInStock.Find(x => x.item == item.item).amount > 0)
        {
            itemsInStock.Find(x => x.item == item.item).amount--;
            itemBasket.Find(x => x.item == item.item).amount++;
            total += item.price;
        }
        else
        {
            Debug.Log("Not enough items in stock");
        }
    }

    public void RemoveFromBasket(ShopItem item)
    {
        if (itemBasket.Find(x => x.item == item.item).amount > 0)
        {
            itemBasket.Find(x => x.item == item.item).amount--;
            itemsInStock.Find(x => x.item == item.item).amount++;
            total -= item.price;
        }
        else
        {
            Debug.Log("Not enough items in basket");
        }
    }

    public void RemoveAllItemFromBasket(ShopItem item)
    {
        total -= itemBasket.Find(x => x.item == item.item).amount * item.price;
        itemsInStock.Find(x => x.item == item.item).amount += itemBasket.Find(x => x.item == item.item).amount;
        itemBasket.Find(x => x.item == item.item).amount = 0;
    }

    public void RemoveAllBasketBack()
    {
        foreach (ShopItem item in itemBasket)
        {
            itemsInStock.Find(x => x.item == item.item).amount += item.amount;
            item.amount = 0;
        }
        total = 0;
    }

    public void RemoveAllBasket()
    {
        foreach (ShopItem item in itemBasket)
        {
            item.amount = 0;
        }
        total = 0;
    }

    public void ConfirmBuy()
    {
        string log = "Items bought: ";
        if (Player.Player.Instance.Money >= total)
        {
            Player.Player.Instance.Money -= total;
            foreach (ShopItem item in itemBasket)
            {
                if (item.amount > 0)
                {
                    switch (item.item)
                    {
                        case Weapon weapon:
                        case Armor armor:
                            Inventory.Instance.AddItem(item.item);
                            break;
                        default:
                            Inventory.Instance.AddItems(item.item, item.amount);
                            break;
                    }
                    log += item.item.itemName + " (" + item.amount + "), ";
                }
            }
            RemoveAllBasket();
            Debug.Log(log);
        }
        else
        {
            Debug.Log("You don't have enough money, you need " + total);
        }
    }
}
