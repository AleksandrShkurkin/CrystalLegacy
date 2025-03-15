using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed Potion Lvl", menuName = "Items/Potions/Speed" )]
public class SpeedPotion : Potion
{
    private CooldownManager speedCooldown = new CooldownManager(5.1f);
    public override void UsePotion(Player player)
    {
        if (amountStacked > 0 && speedCooldown.IsCooldownFinished(Time.time))
        {
            player.MoveSpeed += potionEffectValue;
            speedCooldown.InitiateCooldown(Time.time);
            player.StartCoroutine(ResetSpeed(player));
            amountStacked--;
        }
    }

    private IEnumerator ResetSpeed(Player player)
    {
        yield return new WaitForSeconds(5f);
        player.MoveSpeed -= potionEffectValue;
    }
}
