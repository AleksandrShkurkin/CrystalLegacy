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
        playerHealth.text = "Health: " + Player.Player.Instance.Health.ToString() + "/" + (100 + (int)(100 * (Player.Player.Instance.Level / 10.0f))).ToString();
        playerDamage.text = "Damage: " + Player.Player.Instance.AttackDamage.ToString();
        playerDefence.text = "Defence: " + Player.Player.Instance.Defense.ToString();
        playerLevel.text = "Level: " + Player.Player.Instance.Level.ToString() + " (" + Player.Player.Instance.Exp.ToString() + "/" + (100 + (Player.Player.Instance.Level * 50)).ToString() + ")";
        playerMana.text = "Mana: " + Player.Player.Instance.Mana.ToString();
    }
}
