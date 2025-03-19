using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGearSlot : MonoBehaviour
{
    public Item item;
    public Button button;
    public Image itemImage;
    public TextMeshProUGUI itemType;

    private void Start()
    {
        button.onClick.AddListener(SelectItem);
    }

    public void SetItem (Item item)
    {
        this.item = item;
        itemImage.sprite = item.itemIcon;
    }

    private void SelectItem()
    {
        UIActiveGear.Instance.SelectSlot(this);
        if (UIChangeGear.Instance != null)
            UIChangeGear.Instance.SelectSlot(this);
    }
}
