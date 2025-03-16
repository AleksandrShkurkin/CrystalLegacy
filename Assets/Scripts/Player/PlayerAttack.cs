using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject hitboxArea;
    private bool canAttack = true;
    private CooldownManager attackCooldown;
    private CooldownManager durationCooldown;
    private float cooldownTime = 0.5f;
    private float attackDuration = 0.1f;

    void Start()
    {
        attackCooldown = new CooldownManager(cooldownTime + attackDuration);
        durationCooldown = new CooldownManager(attackDuration);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            if (Inventory.Instance.GetWeaponSelected() && Inventory.Instance.GetMeleeWeapon() != null)
            {
                Inventory.Instance.GetMeleeWeapon().Attack(this);
                durationCooldown.InitiateCooldown(Time.time);
                attackCooldown.InitiateCooldown(Time.time);
            }
            else if (!Inventory.Instance.GetWeaponSelected() && Inventory.Instance.GetRangedWeapon() != null
            && Player.Instance.Mana > 0)
            {
                Weapon weaponRanged = Inventory.Instance.GetRangedWeapon();
                weaponRanged.Attack(this);
                if (weaponRanged.GetType() == typeof(WandPlasma))
                    attackCooldown.InitiateCooldown(Time.time - attackDuration);
                else
                    attackCooldown.InitiateCooldown(Time.time + cooldownTime * 5);
                canAttack = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.Instance.SwapWeapons();
        }

        if (durationCooldown.IsCooldownFinished(Time.time))
        {
            hitboxArea.SetActive(false);
        }

        if (attackCooldown.IsCooldownFinished(Time.time))
        {
            canAttack = true; 
        }
    }

    public void ActivateHitbox(int meleeDamage)
    {
        canAttack = false;
        hitboxArea.SetActive(true);
        hitboxArea.GetComponent<MeleeAttack>().SetDamage(meleeDamage + Player.Instance.AttackDamage);
    }
}
