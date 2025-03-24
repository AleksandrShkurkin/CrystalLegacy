using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChangeGear : MonoBehaviour
{
    public static UIChangeGear Instance { get; private set; }

    public Transform gearPanel;
    public Image currentGearImage;
    public Image selectedGearImage;
    public TextMeshProUGUI currentGearType;
    public TextMeshProUGUI selectedGearType;
    public GameObject gearSlotPrefab;
    private UIGearSlot currentGear;
    private UIGearSlot changeGear;
    public Button backButton;
    public Button selectButton;
    public TextMeshProUGUI currentGearText;
    public TextMeshProUGUI selectedGearText;
    public GameObject activePanel;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        backButton.onClick.AddListener(BackToActive);
        selectButton.onClick.AddListener(ChangeItemInventory);
    }

    void OnEnable()
    {
        currentGear = UIActiveGear.Instance.selectedSlot;
        currentGearImage.sprite = currentGear.item.itemIcon;
        switch (currentGear.item)
        {
            case Weapon weapon:
                currentGearType.text = weapon.weaponType.ToString();
                currentGearText.text = weapon.itemName + "\nDamage: " + weapon.weaponDamage.ToString();
                break;
            case Armor armor:
                currentGearType.text = armor.armorType.ToString();
                currentGearText.text = armor.itemName + "\nDefense: " + armor.armorDefense.ToString();
                break;
            case Potion potion:
                currentGearType.text = potion.potionType.ToString();
                currentGearText.text = potion.itemName + "\nEffect: " + potion.potionEffectValue.ToString();
                break;
        }
        UpdateChangeUI();
        changeGear = null;
        selectedGearImage.sprite = null;
        selectedGearType.text = "";
        selectedGearText.text = "";
        selectButton.interactable = false;
    }

    public void UpdateChangeUI()
    {
        foreach (Transform child in gearPanel)
        {
            Destroy(child.gameObject);
        }

        switch (currentGear.item)
        {
            case Weapon weapon:
                foreach (Weapon w in Inventory.Instance.weapons)
                {
                    if (w.weaponType == weapon.weaponType && w != weapon)
                    {
                        GameObject slotObj = Instantiate(gearSlotPrefab, gearPanel);
                        UIGearSlot slot = slotObj.GetComponent<UIGearSlot>();
                        slot.SetItem(w);
                        slot.itemType.text = w.weaponType.ToString();
                    }
                }
                break;
            case Armor armor:
                foreach (Armor a in Inventory.Instance.armor)
                {
                    if (a.armorType == armor.armorType && a != armor)
                    {
                        GameObject slotObj = Instantiate(gearSlotPrefab, gearPanel);
                        UIGearSlot slot = slotObj.GetComponent<UIGearSlot>();
                        slot.SetItem(a);
                        slot.itemType.text = a.armorType.ToString();
                    }
                }
                break;
            case Potion potion:
                foreach (Potion p in Inventory.Instance.potions)
                {
                    if (p.potionType == potion.potionType && p != potion)
                    {
                        GameObject slotObj = Instantiate(gearSlotPrefab, gearPanel);
                        UIGearSlot slot = slotObj.GetComponent<UIGearSlot>();
                        slot.SetItem(p);
                        slot.itemType.text = p.potionType.ToString();
                    }
                }
                break;
        }
    }

    public void SelectSlot(UIGearSlot slot)
    {
        if (changeGear == slot)
        {
            changeGear = null;
            selectedGearImage.sprite = null;
            selectedGearType.text = "";
            selectedGearText.text = "";
            selectButton.interactable = false;
            return;
        }
        changeGear = slot;
        selectedGearImage.sprite = slot.item.itemIcon;
        switch (slot.item)
        {
            case Weapon weapon:
                selectedGearType.text = weapon.weaponType.ToString();
                selectedGearText.text = weapon.itemName + "\nDamage: " + weapon.weaponDamage.ToString();
                break;
            case Armor armor:
                selectedGearType.text = armor.armorType.ToString();
                selectedGearText.text = armor.itemName + "\nDefense: " + armor.armorDefense.ToString();
                break;
            case Potion potion:
                selectedGearType.text = potion.potionType.ToString();
                selectedGearText.text = potion.itemName + "\nEffect: " + potion.potionEffectValue.ToString();
                break;
        }
        selectButton.interactable = true;
    }

    private void BackToActive()
    {
        gameObject.SetActive(false);
        activePanel.SetActive(true);
    }

    private void ChangeItemInventory()
    {
        switch (changeGear.item)
        {
            case Weapon weapon:
                Inventory.Instance.EquipWeapon(weapon);
                break;
            case Armor armor:
                Inventory.Instance.EquipArmor(armor);
                break;
            case Potion potion:
                Inventory.Instance.EquipPotion(potion);
                break;
        }
        BackToActive();
    }
}
