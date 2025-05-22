using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public TextMeshProUGUI potionText;
    public TextMeshProUGUI weaponText;

    public List<Weapon> weapons = new List<Weapon>();
    public List<Armor> armor = new List<Armor>();
    public List<Potion> potions = new List<Potion>();
    public List<Materials> materials = new List<Materials>();
    public List<CompanionData> unlockedCompanions = new List<CompanionData>();

    private bool meleeSelected = true;
    private Weapon currentMeleeWeapon = null;
    private Weapon currentRangedWeapon = null;
    private Armor currentHelmet = null;
    private Armor currentChest = null;
    private Armor currentPants = null;
    private Armor currentBoots = null;
    private Potion currentHealthPotion = null;
    private Potion currentManaPotion = null;
    private Potion currentSpeedPotion = null;
    private Potion currentStrengthPotion = null;
    private Potion currentPotion = null;
    private int currentCompanionIndex = 0;
    private GameObject? currentCompanionObject = null;

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

    public Potion GetHealthPotion()
    {
        return currentHealthPotion;
    }

    public Potion GetManaPotion()
    {
        return currentManaPotion;
    }

    public Potion GetSpeedPotion()
    {
        return currentSpeedPotion;
    }

    public Potion GetStrengthPotion()
    {
        return currentStrengthPotion;
    }

    public bool GetWeaponSelected()
    {
        return meleeSelected;
    }

    public CompanionData GetCompanionActive()
    {
        return unlockedCompanions[currentCompanionIndex];
    }

    public void SwapWeapons()
    {
        meleeSelected = !meleeSelected;
    }

    public void ChangePotion()
    {
        List<Potion> potionSequence = new List<Potion>{
            currentHealthPotion,
            currentManaPotion,
            currentSpeedPotion,
            currentStrengthPotion
        };
        int currentIndex = potionSequence.IndexOf(currentPotion);

        currentPotion = potionSequence[(currentIndex + 1) % potionSequence.Count];
    }

    public void NextCompanion()
    {
        if (unlockedCompanions.Count == 1) return;

        if (currentCompanionObject != null)
        {
            Destroy(currentCompanionObject);
        }

        currentCompanionIndex = (currentCompanionIndex + 1) % unlockedCompanions.Count;

        if (unlockedCompanions[currentCompanionIndex] != null)
        {
            currentCompanionObject = Instantiate(unlockedCompanions[currentCompanionIndex].companionPrefab, transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle.normalized * 3f), Quaternion.identity);
            Player.Player.Instance.ApplyCompanionEffect(unlockedCompanions[currentCompanionIndex]);
        }
        else
        {
            Player.Player.Instance.ResetCompanionEffect();
        }
    }

    public void PrevCompanion()
    {
        if (unlockedCompanions.Count == 1) return;

        if (currentCompanionObject != null)
        {
            Destroy(currentCompanionObject);
        }

        currentCompanionIndex = (currentCompanionIndex - 1 + unlockedCompanions.Count) % unlockedCompanions.Count;

        if (unlockedCompanions[currentCompanionIndex] != null)
        {
            currentCompanionObject = Instantiate(unlockedCompanions[currentCompanionIndex].companionPrefab, transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle.normalized * 3f), Quaternion.identity);
            Player.Player.Instance.ApplyCompanionEffect(unlockedCompanions[currentCompanionIndex]);
        }
        else
        {
            Player.Player.Instance.ResetCompanionEffect();
        }
    }

    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        EquipPotion(potions[1]);
        EquipPotion(potions[2]);
        EquipPotion(potions[3]);
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].amountStacked = 0;
        }
        unlockedCompanions.Insert(0, null);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentPotion.UsePotion(Player.Player.Instance);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangePotion();
        }
        if (meleeSelected)
        {
            weaponText.text = $"Weapon: {currentMeleeWeapon.itemName}";
        }
        else
        {
            weaponText.text = $"Weapon: {currentRangedWeapon.itemName}";
        }
        potionText.text = $"Potion: {currentPotion.itemName} ({currentPotion.amountStacked})";
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

    public Materials GetMaterial(MaterialType type)
    {
        return materials.Find(m => m.materialType == type);
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
        Player.Player.Instance.Defense += armor.armorDefense;
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
        Player.Player.Instance.Defense -= armor.armorDefense;
        Debug.Log("Defense: " + Player.Player.Instance.Defense);
    }

    public void EquipPotion(Potion potion)
    {
        if (potion.potionType == PotionType.Health)
        {
            currentHealthPotion = potion;
        }
        if (potion.potionType == PotionType.Mana)
        {
            currentManaPotion = potion;
        }
        if (potion.potionType == PotionType.Speed)
        {
            currentSpeedPotion = potion;
        }
        if (potion.potionType == PotionType.Strength)
        {
            currentStrengthPotion = potion;
        }
        if (currentPotion == null || (currentPotion != null && potion.potionType == currentPotion.potionType))
        {
            currentPotion = potion;
        }
    }
}
