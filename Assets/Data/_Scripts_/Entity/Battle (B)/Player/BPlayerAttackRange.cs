using UnityEngine;

public class BPlayerAttackRange : BEntityAttackRange
{
    protected override void Start()
    {
        base.Start();
        PlayerSO playerData = BattleManager.Instance.PlayerData;

        if (playerData.Weapon.EquipSO != null)
        {
            WeaponSO weaponSO = playerData.Weapon.EquipSO as WeaponSO;

            current = weaponSO.AttackRange;
        }
        else
        {
            current = AttackRange.Melee;
        }
    }
}