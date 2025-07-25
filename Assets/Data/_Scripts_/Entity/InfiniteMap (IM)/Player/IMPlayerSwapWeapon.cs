using UnityEngine;

public class IMPlayerSwapWeapon : EntityComponent
{
    private IMPlayer player;
    private InfiniteMapManager infiniteMapManager;
    private PlayerSO playerData;

    protected override void Start()
    {
        base.Start();

        player = Entity as IMPlayer;
        infiniteMapManager = InfiniteMapManager.Instance;
        playerData = infiniteMapManager.PlayerData;

        infiniteMapManager.Inventory.EquipWearing.OnEquipWearing += SwapWeapon;

        if (playerData.Weapon != null && playerData.Weapon.EquipSO != null)
        {
            SwapWeapon(playerData.Weapon);
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