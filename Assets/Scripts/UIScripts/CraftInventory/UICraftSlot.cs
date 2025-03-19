using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICraftSlot : MonoBehaviour
{
    public Recipe recipe;
    public Button button;
    public Image itemImage;
    public Transform materialPanel;
    public GameObject materialPrefab;
    public TextMeshProUGUI craftItemName;
    public TextMeshProUGUI craftItemDesc;

    private void Start()
    {
        button.onClick.AddListener(SelectSlot);
    }

    private void SelectSlot()
    {
        UICraft.Instance.SelectSlot(this);
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
        itemImage.sprite = recipe.itemCrafted.itemIcon;
        craftItemName.text = recipe.itemCrafted.itemName;
        craftItemDesc.text = recipe.itemCrafted.itemDescription;

        foreach(MaterialRequirement materialReq in recipe.materialsRequired)
        {
            Materials material = Inventory.Instance.GetMaterial(materialReq.materialType);

            GameObject materialReqObj = Instantiate(materialPrefab, materialPanel);
            Image[] images = materialReqObj.GetComponentsInChildren<Image>(true);
            Image materialIcon = images[1];
            materialIcon.sprite = material.itemIcon;
            materialReqObj.GetComponentInChildren<TextMeshProUGUI>().text = materialReq.amountRequired.ToString();
        }
        
    }
}
