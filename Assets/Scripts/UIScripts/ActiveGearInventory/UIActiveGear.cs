using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIActiveGear : MonoBehaviour
{
    public static UIActiveGear Instance { get; private set; }
    public Transform weaponsPanel;
    public Transform armorPanel;
    public Transform potionsPanel;
    public GameObject gearSlotPrefab;
    public Button changeButton;
    public UIGearSlot selectedSlot;
    public GameObject changePanel;
    public Image companionImage;
    public Button nextCompanion;
    public Button prevCompanion;
    public TextMeshProUGUI compName;
    public TextMeshProUGUI compDescription;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        UpdateGearUI();
        selectedSlot = null;
        changeButton.interactable = false;
    }

    private void Start()
    {
        changeButton.onClick.AddListener(ChangeItem);
        nextCompanion.onClick.AddListener(NextCompanion);
        prevCompanion.onClick.AddListener(PrevCompanion);
    }

    public void UpdateGearUI()
    {
        foreach (Transform child in weaponsPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in armorPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in potionsPanel)
        {
            Destroy(child.gameObject);
        }

        FillWeaponPanel();
        FillArmorPanel();
        FillPotionsPanel();
        ShowCompanion();
    }

    private void FillWeaponPanel()
    {
        GameObject meleeSlot = Instantiate(gearSlotPrefab, weaponsPanel);
        UIGearSlot meleeGearSlot = meleeSlot.GetComponent<UIGearSlot>();
        meleeGearSlot.SetItem(Inventory.Instance.GetMeleeWeapon());
        meleeGearSlot.itemType.text = "Melee";

        GameObject rangedSlot = Instantiate(gearSlotPrefab, weaponsPanel);
        UIGearSlot rangedGearSlot = rangedSlot.GetComponent<UIGearSlot>();
        rangedGearSlot.SetItem(Inventory.Instance.GetRangedWeapon());
        meleeGearSlot.itemType.text = "Ranged";
    }

    private void FillArmorPanel()
    {
        GameObject helmetSlot = Instantiate(gearSlotPrefab, armorPanel);
        UIGearSlot helmetGearSlot = helmetSlot.GetComponent<UIGearSlot>();
        helmetGearSlot.SetItem(Inventory.Instance.GetHelmet());
        helmetGearSlot.itemType.text = "Helmet";

        GameObject chestPlateSlot = Instantiate(gearSlotPrefab, armorPanel);
        UIGearSlot chestPlateGearSlot = chestPlateSlot.GetComponent<UIGearSlot>();
        chestPlateGearSlot.SetItem(Inventory.Instance.GetChest());
        chestPlateGearSlot.itemType.text = "Chest";

        GameObject pantsSlot = Instantiate(gearSlotPrefab, armorPanel);
        UIGearSlot pantsGearSlot = pantsSlot.GetComponent<UIGearSlot>();
        pantsGearSlot.SetItem(Inventory.Instance.GetPants());
        pantsGearSlot.itemType.text = "Pants";

        GameObject bootsSlot = Instantiate(gearSlotPrefab, armorPanel);
        UIGearSlot bootsGearSlot = bootsSlot.GetComponent<UIGearSlot>();
        bootsGearSlot.SetItem(Inventory.Instance.GetBoots());
        bootsGearSlot.itemType.text = "Boots";
    }

    private void FillPotionsPanel()
    {
        GameObject healthSlot = Instantiate(gearSlotPrefab, potionsPanel);
        UIGearSlot healthGearSlot = healthSlot.GetComponent<UIGearSlot>();
        healthGearSlot.SetItem(Inventory.Instance.GetHealthPotion());
        healthGearSlot.itemType.text = "Health";

        GameObject manaSlot = Instantiate(gearSlotPrefab, potionsPanel);
        UIGearSlot manaGearSlot = manaSlot.GetComponent<UIGearSlot>();
        manaGearSlot.SetItem(Inventory.Instance.GetManaPotion());
        manaGearSlot.itemType.text = "Mana";

        GameObject speedSlot = Instantiate(gearSlotPrefab, potionsPanel);
        UIGearSlot speedGearSlot = speedSlot.GetComponent<UIGearSlot>();
        speedGearSlot.SetItem(Inventory.Instance.GetSpeedPotion());
        speedGearSlot.itemType.text = "Speed";

        GameObject strengthSlot = Instantiate(gearSlotPrefab, potionsPanel);
        UIGearSlot strengthGearSlot = strengthSlot.GetComponent<UIGearSlot>();
        strengthGearSlot.SetItem(Inventory.Instance.GetStrengthPotion());
        strengthGearSlot.itemType.text = "Strength";
    }

    private void ShowCompanion()
    {
        CompanionData currCompanion = Inventory.Instance.GetCompanionActive();
        if (currCompanion != null)
        {
            companionImage.sprite = currCompanion.icon;
            compName.text = currCompanion.companionName;
            compDescription.text = "Damage: " + currCompanion.companionPrefab.GetComponent<Companion>().AttackDamage +
            "\n" + currCompanion.effectType.ToString() + ": " + currCompanion.effectValue.ToString();
        }
        else
        {
            companionImage.sprite = null;
            compName.text = "Name: none";
            compDescription.text = "None";
        }
    }

    public void SelectSlot(UIGearSlot slot)
    {
        if (selectedSlot == slot)
        {
            selectedSlot = null;
            changeButton.interactable = false;
            return;
        }
        selectedSlot = slot;
        changeButton.interactable = true;
    }

    private void ChangeItem()
    {
        gameObject.SetActive(false);
        changePanel.SetActive(true);
    }

    private void NextCompanion()
    {
        Inventory.Instance.NextCompanion();
        ShowCompanion();
    }

    private void PrevCompanion()
    {
        Inventory.Instance.PrevCompanion();
        ShowCompanion();
    }
}
