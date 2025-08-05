using UnityEngine;

public class IMPlayerAnim : EntityAnim
{
    [SerializeField] private RuntimeAnimatorController origin;
    [SerializeField] private AnimatorOverrideController staffOverride;
    private InfiniteMapManager infiniteMapManager;
    private PlayerSO playerData;

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;
        playerData = infiniteMapManager.PlayerData;

        infiniteMapManager.Inventory.EquipWearing.OnEquipWearing += SwapOverride;
        infiniteMapManager.Equipment.Unequip.OnEquipDisarming += ResetOverride;

        if (playerData.Weapon != null && playerData.Weapon.EquipSO != null)
        {
            SwapOverride(playerData.Weapon);
        }

    }

    public void MeleeAttack()
    {
        Entity.Animator.SetTrigger("melee_attack");
    }

    public void RangedAttack()
    {
        Entity.Animator.SetTrigger("ranged_attack");
    }

    public void IdleToJump()
    {
        Entity.Animator.Play("IdleToJump");
    }

    public void Jump()
    {
        Entity.Animator.Play("Jump");
    }

    public void Fall()
    {
        Entity.Animator.Play("Fall");
    }

    public void FallToIlde()
    {
        Entity.Animator.Play("FallToIdle");
    }

    public void SwapOverride(InventoryEquip weapon)
    {
        if (weapon.EquipSO is not WeaponSO) return;

        WeaponSO weaponSO = (WeaponSO)weapon.EquipSO;

        SetWeaponOverride(weaponSO);
    }

    public void ResetOverride(InventoryEquip weapon)
    {
        Entity.Animator.runtimeAnimatorController = origin;
    }

    public void SetWeaponOverride(WeaponSO weaponSO)
    {
        switch (weaponSO.WeaponType)
        {
            case WeaponType.Sword:
                Entity.Animator.runtimeAnimatorController = origin;
                break;
            case WeaponType.Staff:
                Entity.Animator.runtimeAnimatorController = staffOverride;
                break;
        }
    }
}