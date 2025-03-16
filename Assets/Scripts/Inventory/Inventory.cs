using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI potionText;
    [SerializeField] private TextMeshProUGUI weaponText;

    public List<Weapon> weapons = new List<Weapon>();
    public List<Armor> armor = new List<Armor>();
    public List<Potion> potions = new List<Potion>();
    public List<Materials> materials = new List<Materials>();

    private bool meleeSelected = true;
    private Weapon currentMeleeWeapon = null;
    private Weapon currentRangedWeapon = null;
    private Armor currentHelmet = null;
    private Armor currentChest = null;
    private Armor currentPants = null;
    private Armor currentBoots = null;
    private Potion currentPotion = null;
    private int potionIndex = 0;

    public Weapon GetMeleeWeapon()
    {
        return currentMeleeWeapon;
    }

    public Weapon GetRangedWeapon()
    {
        return currentRangedWeapon;
    }

    public Armor GetHelmet()
    {
        return currentHelmet;
    }

    public Armor GetChest()
    {
        return currentChest;
    }

    public Armor GetPants()
    {
        return currentPants;
    }

    public Armor GetBoots()
    {
        return currentBoots;
    }

    public bool GetWeaponSelected()
    {
        return meleeSelected;
    }

    public void SwapWeapons()
    {
        meleeSelected = !meleeSelected;
    }

    public void ChangePotion()
    {
        if (potions.Count == 0)
            return;

        int startIndex = (potionIndex + 1) % potions.Count;
        int searchIndex = startIndex;

        do
        {
            if (potions[potionIndex].amountStacked > 0)
            {
                potionIndex = searchIndex;
                EquipPotion(potions[potionIndex]);
                return;
            }

            searchIndex = (searchIndex + 1) % potions.Count;
        } while (searchIndex != potionIndex);

        potionIndex = startIndex;
        EquipPotion(potions[potionIndex]);
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        EquipWeapon(weapons[0]);
        EquipWeapon(weapons[1]);
        for (int i = 0; i < armor.Count; i++)
        {
            EquipArmor(armor[i]);
        }
        for (int i = 0; i < potions.Count; i++)
        {
            potions[i].amountStacked = 0;
        }
        EquipPotion(potions[0]);
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].amountStacked = 0;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentPotion.UsePotion(Player.Instance);
            potionText.text = $"Potion: {currentPotion.itemName} ({currentPotion.amountStacked})";
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangePotion();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (Potion potion in potions)
            {
                potion.amountStacked++;
            }
            potionText.text = $"Potion: {currentPotion.itemName} ({currentPotion.amountStacked})";
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            bool result = Craft.Instance.CraftItem(currentPotion);
            if (result)
            {
                Debug.Log("Crafted: " + currentPotion.itemName);
            }
            else
            {
                Debug.Log("Not enough materials to craft: " + currentPotion.itemName);
            }
        }
        if (meleeSelected)
        {
            weaponText.text = $"Weapon: {currentMeleeWeapon.itemName}";
        }
        else
        {
            weaponText.text = $"Weapon: {currentRangedWeapon.itemName}";
        }
    }

    public void AddItem(Item item)
    {
        switch (item)
        {
            case Weapon weapon:
                if (!weapons.Contains(weapon))
                    weapons.Add(weapon);
                string weapInv = String.Join(", ", weapons.Select(w => w.itemName));
                Debug.Log(weapInv);
                break;
            case Armor armorPeice:
                if (!armor.Contains(armorPeice))
                    armor.Add(armorPeice);
                string armInv = String.Join(", ", armor.Select(a => a.itemName));
                Debug.Log(armInv);
                break;
        }
    }

    public void AddItems(Item item, int amount)
    {
        switch (item)
        {
            case Potion potion:
                if (!potions.Contains(potion))
                {
                    potions.Add(potion);
                }
                potion.amountStacked += amount;
                break;
            case Materials material:
                AddMaterial(material.materialType, amount);
                break;
        }
    }

    public void AddMaterial(MaterialType type, int amount)
    {
        Materials material = materials.Find(m => m.materialType == type);
        material.amountStacked += amount;
    }

    public bool CheckAmount(MaterialType type, int amount)
    {
        Materials material = materials.Find(m => m.materialType == type);
        if (material.amountStacked >= amount)
        {
            return true;
        }
        return false;
    }

    public int GetMaterialAmount(MaterialType type)
    {
        Materials material = materials.Find(m => m.materialType == type);
        return material.amountStacked;
    }

    public void RemoveMaterial(MaterialType type, int amount)
    {
        Materials material = materials.Find(m => m.materialType == type);
        material.amountStacked -= amount;
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (weapon.weaponType == WeaponType.Melee)
        {
            currentMeleeWeapon = weapon;
        }
        else if (weapon.weaponType == WeaponType.Ranged)
        {
            currentRangedWeapon = weapon;
        }
    }

    public void UnequipWeapon(Weapon weapon)
    {
        if (weapon.weaponType == WeaponType.Melee)
        {
            currentMeleeWeapon = null;
        }
        else if (weapon.weaponType == WeaponType.Ranged)
        {
            currentRangedWeapon = null;
        }
    }

    public void EquipArmor(Armor armor)
    {
        if (armor.armorType == ArmorType.Helmet)
        {
            currentHelmet = armor;
        }
        if (armor.armorType == ArmorType.Chestplate)
        {
            currentChest = armor;
        }
        if (armor.armorType == ArmorType.Pants)
        {
            currentPants = armor;
        }
        if (armor.armorType == ArmorType.Boots)
        {
            currentBoots = armor;
        }
        armor.SpecialEffect();
        Player.Instance.Defense += armor.armorDefense;
    }

    public void UnequipArmor(Armor armor)
    {
        if (armor.armorType == ArmorType.Helmet)
        {
            currentHelmet = null;
        }
        if (armor.armorType == ArmorType.Chestplate)
        {
            currentChest = null;
        }
        if (armor.armorType == ArmorType.Pants)
        {
            currentPants = null;
        }
        if (armor.armorType == ArmorType.Boots)
        {
            currentBoots = null;
        }
        armor.SpecialEffectRemove();
        Player.Instance.Defense -= armor.armorDefense;
        Debug.Log("Defense: " + Player.Instance.Defense);
    }

    public void EquipPotion(Potion potion)
    {
        currentPotion = potion;
        potionText.text = $"Potion: {currentPotion.itemName} ({currentPotion.amountStacked})";
    }
}
