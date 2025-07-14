using UnityEngine;

public class BPlayerAttackRange : BEntityAttackRange
{
    protected override void Start()
    {
        base.Start();
        PlayerInfo playerInfo = BattleManager.Instance.MapData.PlayerInfo;

        if (BattleManager.Instance.MapData.MapCanLoad)
        {
            if (playerInfo.Weapon.EquipSO == null)
            {
                current = AttackRange.Melee;
            }
            else
            {
                WeaponSO weaponSO = playerInfo.Weapon.EquipSO as WeaponSO;

                current = weaponSO.AttackRange;
            }

        }
    }
}