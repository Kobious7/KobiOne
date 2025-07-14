using System;
using UnityEngine;

public class PlayerVFXManager : GMono
{
    private static PlayerVFXManager instance;

    public static PlayerVFXManager Instance => instance;

    [SerializeField] private PlayerBasicSlashVFX basicSlashVFX;
    [SerializeField] private PlayerMagicAttackVFX magicAttackVFX;
    [SerializeField] private PlayerSwordA1SHealVFX swordA1S_heal;
    [SerializeField] private PlayerSwordS1SSwordVFX swordS1S_swordVFX;

    protected override void Awake()
    {
        base.Awake();

        if (instance != null) Debug.LogError("Only 1 PlayerVFXManager");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBasicSlashVFX();
        LoadMagicAttackVFX();
        LoadSwordA1SHealVFX();
        LoadSwordS1SSwordVFX();
    }

    private void LoadBasicSlashVFX()
    {
        if (basicSlashVFX != null) return;

        basicSlashVFX = GetComponentInChildren<PlayerBasicSlashVFX>();
    }

    private void LoadMagicAttackVFX()
    {
        if (magicAttackVFX != null) return;

        magicAttackVFX = GetComponentInChildren<PlayerMagicAttackVFX>();
    }

    private void LoadSwordA1SHealVFX()
    {
        if (swordA1S_heal != null) return;

        swordA1S_heal = GetComponentInChildren<PlayerSwordA1SHealVFX>();
    }

    private void LoadSwordS1SSwordVFX()
    {
        if (swordS1S_swordVFX != null) return;

        swordS1S_swordVFX = GetComponentInChildren<PlayerSwordS1SSwordVFX>();
    }

    public void PlayBasicSlashTrailVFX()
    {
        basicSlashVFX.PlayTrail();
    }

    public void StopBasicSlashTrailVFX()
    {
        basicSlashVFX.StopTrail();
    }

    public void PlayBasicHitParticleVFX()
    {
        basicSlashVFX.PlayHitParticle();
    }

    public void PlayMagicAttractorVFX()
    {
        magicAttackVFX.PlayMagicAttactor();
    }

    public void PlayeSwordA1SHealingVFX()
    {
        swordA1S_heal.PlayHealParticle();
    }

    public void PlaySwordS1SSwordVFX()
    {
        swordS1S_swordVFX.ActiveSword();
    }

    public void StopSwordS1SSwordVFX()
    {
        swordS1S_swordVFX.InactiveSword();
    }
}