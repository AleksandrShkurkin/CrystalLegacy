using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI playerDamage;
    public TextMeshProUGUI playerDefence;
    public TextMeshProUGUI playerLevel;
    public TextMeshProUGUI playerMana;

    void OnEnable()
    {
        playerHealth.text = "Health: " + Player.Instance.Health.ToString() + "/" + (100 + (int)(100 * (Player.Instance.Level / 10.0f))).ToString();
        playerDamage.text = "Damage: " + Player.Instance.AttackDamage.ToString();
        playerDefence.text = "Defence: " + Player.Instance.Defense.ToString();
        playerLevel.text = "Level: " + Player.Instance.Level.ToString() + " (" + Player.Instance.Exp.ToString() + "/" + (100 + (Player.Instance.Level * 50)).ToString() + ")";
        playerMana.text = "Mana: " + Player.Instance.Mana.ToString();
    }
}
