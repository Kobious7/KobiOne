using UnityEngine;

public class BPlayerWeaponSwap : BEntityComponent
{
    private BPlayer player;

    protected override void Start()
    {
        base.Start();

        player = Entity as BPlayer;
        PlayerInfo playerInfo = BattleManager.Instance.MapData.PlayerInfo;

        if (BattleManager.Instance.MapData.MapCanLoad)
        {
            if (playerInfo.Weapon.EquipSO == null) return;

            SwapWeapon(BattleManager.Instance.MapData.PlayerInfo.Weapon);
        }
        else
        {
            player.MeleeAttack.gameObject.SetActive(true);
            player.AttackPoint.gameObject.SetActive(false);
            player.RangedAttack.gameObject.SetActive(false);
        }

    }

    public void SwapWeapon(InventoryEquip equip)
    {
        WeaponSO weapon = (WeaponSO)equip.EquipSO;

        switch(weapon.AttackRange)
        {
            case AttackRange.Melee:
                player.MeleeAttack.gameObject.SetActive(true);
                player.AttackPoint.gameObject.SetActive(false);
                player.RangedAttack.gameObject.SetActive(false);
                break;
            case AttackRange.Ranged:
                player.MeleeAttack.gameObject.SetActive(false);
                player.AttackPoint.gameObject.SetActive(true);
                player.RangedAttack.gameObject.SetActive(true);
                break;
        }
    }
}