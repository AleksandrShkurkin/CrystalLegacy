using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public static Craft Instance { get; private set; }
    public List<Recipe> recipies = new List<Recipe>();

    private void Awake()
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

    public bool CraftItem(Item item)
    {
        foreach (Recipe recipe in recipies)
        {
            if (recipe.itemCrafted == item)
            {
                foreach (MaterialRequirement material in recipe.materialsRequired)
                {
                    if (!Inventory.Instance.CheckAmount(material.materialType, material.amountRequired))
                    {
                        return false;
                    }
                }
                foreach (MaterialRequirement material in recipe.materialsRequired)
                {
                    Inventory.Instance.RemoveMaterial(material.materialType, material.amountRequired);
                }
                Inventory.Instance.AddItem(item);
                return true;
            }
        }
        return false;
    }
}
