using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlot : MonoBehaviour
{
    public ShopItem shopItem;
    public Button button;
    public Image image;
    public TextMeshProUGUI shopItemName;
    public TextMeshProUGUI shopItemPrice;
    public TextMeshProUGUI shopItemAmount;
    private System.Action<ShopItem> onClickAction;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetItem(ShopItem shopItem, System.Action<ShopItem> clickAction)
    {
        this.shopItem = shopItem;
        onClickAction = clickAction;
        image.sprite = shopItem.item.itemIcon;
        shopItemName.text = shopItem.item.itemName;
        shopItemPrice.text = shopItem.price.ToString();
        shopItemAmount.text = shopItem.amount.ToString();
    }

    private void OnClick()
    {
        onClickAction?.Invoke(shopItem);
        UIShop.Instance.UpdateShopUI();
    }
}
