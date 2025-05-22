using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Craft
{
    public class Craft : MonoBehaviour
    {
        public static Craft Instance { get; private set; }
        public List<Recipe> recipies = new List<Recipe>();

        private void Awake()
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

        public bool CraftItem(Item item)
        {
            foreach (var recipe in recipies.Where(recipe => recipe.itemCrafted == item))
            {
                if (recipe.materialsRequired.Any(material =>
                        !Inventory.Instance.CheckAmount(material.materialType, material.amountRequired)))
                {
                    return false;
                }
                foreach (var material in recipe.materialsRequired)
                {
                    Inventory.Instance.RemoveMaterial(material.materialType, material.amountRequired);
                }
                Inventory.Instance.AddItems(item, 1);
                return true;
            }

            return false;
        }
    }
}
