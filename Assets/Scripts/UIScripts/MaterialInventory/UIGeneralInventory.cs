using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIGeneralInventory : MonoBehaviour
{
    public static UIGeneralInventory Instance { get; private set;}
    public Transform inventoryPanel;
    public GameObject inventorySlotPrefab;
    private UiSlot selectedSlot;
    public TextMeshProUGUI selectedMaterialText;
    public Image materialImage;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        UpdateInventoryUI();
        selectedSlot = null;
        selectedMaterialText.text = "";
        materialImage.sprite = null;
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Materials material in Inventory.Instance.materials)
        {
            GameObject slotObj = Instantiate(inventorySlotPrefab, inventoryPanel);
            UiSlot slot = slotObj.GetComponent<UiSlot>();
            slot.SetMaterial(material);
        }
    }

    public void SelectSlot(UiSlot slot)
    {
        selectedSlot = slot;
        selectedMaterialText.text = slot.material.itemName + "\n" + slot.material.itemDescription;
        materialImage.sprite = slot.material.itemIcon;
    }
}
