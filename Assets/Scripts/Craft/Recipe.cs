using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Items/Craft/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public Item itemCrafted;
    public List<MaterialRequirement> materialsRequired;
}

[System.Serializable]
public class MaterialRequirement
{
    public MaterialType materialType;
    public int amountRequired;
}