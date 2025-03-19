using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiSlot : MonoBehaviour
{
    public Materials material;
    public Button button;
    public Image materialImage;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(SelectMaterial);
    }

    public void SetMaterial(Materials material)
    {
        this.material = material;
        GetComponentInChildren<TextMeshProUGUI>().text = material.amountStacked.ToString();
        materialImage.sprite = material.itemIcon;
    }

    private void SelectMaterial()
    {
        UIGeneralInventory.Instance.SelectSlot(this);
    }
}
