using UnityEngine;
using System.Linq;

public class PlayerVFXHandler : GMono
{
    [SerializeField] private PlayerVFXManager vfxManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadVFXManager();
    }

    private void LoadVFXManager()
    {
        if (vfxManager != null) return;

        vfxManager = transform.parent.GetComponentInChildren<PlayerVFXManager>();
    }

    private void PlayBasicSlashTrail()
    {
        vfxManager.PlayBasicSlashTrailVFX();
    }

    private void StopBasicSlashTrail()
    {
        vfxManager.StopBasicSlashTrailVFX();
    }

    private void PlayBasicSlashHit()
    {
        vfxManager.PlayBasicHitParticleVFX();
    }

    private void PlayMagicAttractor()
    {
        vfxManager.PlayMagicAttractorVFX();
    }
}