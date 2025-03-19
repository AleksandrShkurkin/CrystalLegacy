using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public static UIShop Instance { get; private set; }
    private Shop activeShop;
    public Transform stockPanel;
    public Transform basketPanel;
    public GameObject shopSlotPrefab;
    public Button buyButton;
    public Button clearButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buyButton.onClick.AddListener(BuyBasket);
        clearButton.onClick.AddListener(ClearBasket);
    }

    public void OpenShop(Shop shop)
    {
        activeShop = shop;
        UpdateShopUI();
        buyButton.interactable = false;
        clearButton.interactable = false;
    }

    public void CloseShop()
    {
        activeShop.RemoveAllBasket();
        activeShop = null;
    }

    public void UpdateShopUI()
    {
        buyButton.interactable = false;
        clearButton.interactable = false;
        foreach (Transform child in stockPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in basketPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem shopItem in activeShop.itemsInStock)
        {
            if (shopItem.amount > 0)
            {
                GameObject slotStockObj = Instantiate(shopSlotPrefab, stockPanel);
                slotStockObj.GetComponent<ShopItemSlot>().SetItem(shopItem, activeShop.AddToBasket);
            }
        }
        foreach (ShopItem shopItem in activeShop.itemBasket)
        {
            if (shopItem.amount > 0)
            {
                GameObject slotStockObj = Instantiate(shopSlotPrefab, basketPanel);
                slotStockObj.GetComponent<ShopItemSlot>().SetItem(shopItem, activeShop.RemoveFromBasket);
                buyButton.interactable = true;
                clearButton.interactable = true;
            }
        }
    }

    private void ClearBasket()
    {
        activeShop.RemoveAllBasketBack();
        UpdateShopUI();
    }

    private void BuyBasket()
    {
        activeShop.ConfirmBuy();
        UpdateShopUI();
    }
}
