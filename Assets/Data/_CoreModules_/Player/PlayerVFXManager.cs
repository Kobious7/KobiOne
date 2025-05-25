using System;
using UnityEngine;

public class PlayerVFXManager : GMono
{
    [SerializeField] private PlayerBasicSlashVFX basicSlashVFX;
    [SerializeField] private PlayerMagicAttackVFX magicAttackVFX;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBasicSlashVFX();
        LoadMagicAttackVFX();
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
}