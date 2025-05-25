using UnityEngine;

public class PlayerSwapWeapon : PlayerComponent
{
    [SerializeField] private AttackRange currentAttackRange;

    public AttackRange CurrentAttackRange => currentAttackRange;
    protected override void Start()
    {
        base.Start();

        if (Player.ActiveScene == "InfiniteMap")
        {
            Game.Instance.Inventory.EquipWearing.OnEquipWearing += SwapWeapon;
        }

        Player.MeleeAttack.gameObject.SetActive(false);
        Player.AttackPoint.gameObject.SetActive(true);
        Player.RangedAttack.gameObject.SetActive(true);
    }

    public void SwapWeapon(InventoryEquip equip)
    {
        WeaponSO weapon = (WeaponSO)equip.EquipSO;

        switch(weapon.AttackRange)
        {
            case AttackRange.Melee:
                Player.MeleeAttack.gameObject.SetActive(true);
                Player.AttackPoint.gameObject.SetActive(false);
                Player.RangedAttack.gameObject.SetActive(false);
                currentAttackRange = AttackRange.Melee;
                break;
            case AttackRange.Ranged:
                Player.MeleeAttack.gameObject.SetActive(false);
                Player.AttackPoint.gameObject.SetActive(true);
                Player.RangedAttack.gameObject.SetActive(true);
                currentAttackRange = AttackRange.Ranged;
                break;
        }
    }
}