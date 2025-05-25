using System.Collections;
using UnityEngine;


public class PlayerAnim : PlayerComponent
{
    [SerializeField] private RuntimeAnimatorController origin;
    [SerializeField] private AnimatorOverrideController staffOverride;

    protected override void Start()
    {
        base.Start();

        Player.Animator.runtimeAnimatorController = staffOverride;

        if (Player.ActiveScene == "InfiniteMap")
        {
            Game.Instance.Inventory.EquipWearing.OnEquipWearing += SwapOverride;
        }
    }

    public void Idle()
    {
        Player.Animator.SetInteger("state", 0);
    }

    public void Run()
    {
        Player.Animator.SetInteger("state", 1);
    }

    public void MeleeAttack()
    {
        Player.Animator.SetTrigger("melee_attack");
    }

    public void RangedAttack()
    {
        Player.Animator.SetTrigger("ranged_attack");
    }

    public void IdleToJump()
    {
        Player.Animator.Play("IdleToJump");
    }

    public void Jump()
    {
        Player.Animator.Play("Jump");
    }

    public void Fall()
    {
        Player.Animator.Play("Fall");
    }

    public void FallToIlde()
    {
        Player.Animator.Play("FallToIdle");
    }

    public IEnumerator WaitAnim(string name)
    {
        yield return new WaitForSeconds(GetAnimDuration(name));
    }

    public float GetAnimDuration(string name)
    {
        foreach (AnimationClip clip in Player.Animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }

        return 1;
    }

    public void SwapOverride(InventoryEquip weapon)
    {
        WeaponSO weaponSO = (WeaponSO)weapon.EquipSO;

        SetWeaponOverride(weaponSO);
    }

    public void SetWeaponOverride(WeaponSO weaponSO)
    {
        switch(weaponSO.WeaponType)
        {
            case WeaponType.Sword:
                Player.Animator.runtimeAnimatorController = origin;
                break;
            case WeaponType.Staff:
                Player.Animator.runtimeAnimatorController = staffOverride;
                break;
        }
    }
}