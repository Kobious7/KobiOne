using System.Collections;
using UnityEngine;


public class BPlayerAnim : BEntityAnim
{
    [SerializeField] private RuntimeAnimatorController origin;
    [SerializeField] private AnimatorOverrideController staffOverride;

    protected override void Start()
    {
        base.Start();

        PlayerSO playerData = BattleManager.Instance.PlayerData;

        if (playerData.Weapon.EquipSO != null)
        {
            SwapOverride(playerData.Weapon);
        }
        else
        {
            Entity.Animator.runtimeAnimatorController = origin;
        }

        Battle.Instance.OnPlayerLost += Die;
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

    public void BeingHit()
    {
        Entity.Animator.Play("BeingHit");
    }

    public void Die()
    {
        Entity.Animator.Play("Die");
    }

    public void TileSkillCast()
    {
        Entity.Animator.Play("SwordRSCast");
    }

    public void SwordA1SCast1()
    {
        Entity.Animator.Play("SwordA1SCast1");
        Entity.Animator.SetInteger("state", 161);
    }

    public void SwordA1SDashForward()
    {
        Entity.Animator.Play("SwordA1SDashForward");
    }
    
    public void SwordA1SCast2()
    {
        Entity.Animator.Play("SwordA1SCast2");
    }

    public void SwordA1SMoveBack()
    {
        Entity.Animator.Play("SwordA1SMoveBack");
    }

    public IEnumerator SwordS1SCast()
    {
        Entity.Animator.Play("SwordS1SCast");
        yield return StartCoroutine(WaitAnim("SwordS1SCast"));
        Entity.Animator.Play("SwordS1S_FX_Override");
    }

    public void EndSwordS1SFX()
    {
        Entity.Animator.Play("SwordS1S_Fade_Override");
    }

    public IEnumerator SwordS3SCast()
    {
        Entity.Animator.Play("SwordS3SCast");
        yield return StartCoroutine(WaitAnim("SwordS3Cast"));
        Entity.Animator.Play("Artifact_Override");
    }

    public void EndSwordS3SFX()
    {
        Entity.Animator.Play("Empty_Artifact");
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
                Entity.Animator.runtimeAnimatorController = origin;
                break;
            case WeaponType.Staff:
                Entity.Animator.runtimeAnimatorController = staffOverride;
                break;
        }
    }
}