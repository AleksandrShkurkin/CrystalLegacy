using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraft : MonoBehaviour
{
    public static UICraft Instance { get; private set; }
    public Transform craftPanel;
    public GameObject craftSlotPrefab;
    private UICraftSlot selectedSlot;
    public Button craftButton;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        UpdateCraftUI();
        selectedSlot = null;
        craftButton.interactable = false;
    }

    private void Start()
    {
        craftButton.onClick.AddListener(CraftTry);
    }

    public void UpdateCraftUI()
    {
        foreach (Transform child in craftPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Recipe recipe in Craft.Instance.recipies)
        {
            GameObject slotObj = Instantiate(craftSlotPrefab, craftPanel);
            UICraftSlot slot = slotObj.GetComponent<UICraftSlot>();
            slot.SetRecipe(recipe);
        }
    }

    public void SelectSlot(UICraftSlot slot)
    {
        if (selectedSlot == slot)
        {
            selectedSlot = null;
            craftButton.interactable = false;
            return;
        }
        selectedSlot = slot;
        craftButton.interactable = true;
    }

    private void CraftTry()
    {
        bool success = Craft.Instance.CraftItem(selectedSlot.recipe.itemCrafted);
        if (!success)
        {
            Debug.Log("Not enough materials in inventory!");
        }
    }
}
