using System.Xml.Serialization;
using UnityEngine;

public class PlayerBasicSlashVFX : GMono
{
    [SerializeField] private PlayerBasicSlashTrailVFX trailVFX;
    [SerializeField] private PlayerBasicHitParticleVFX hitParticleVFX;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTrailVFX();
        LoadHitParticleVFX();
    }

    private void LoadTrailVFX()
    {
        if (trailVFX != null) return;

        trailVFX = GetComponentInChildren<PlayerBasicSlashTrailVFX>();
    }

    private void LoadHitParticleVFX()
    {
        if (hitParticleVFX != null) return;

        hitParticleVFX = GetComponentInChildren<PlayerBasicHitParticleVFX>();
    }

    public void PlayTrail()
    {
        trailVFX.PlayVFX();
    }

    public void StopTrail()
    {
        trailVFX.StopVFX();
    }

    public void PlayHitParticle()
    {
        if (!PlayerEventsInAnim.Instance.Hit) return;

        StartCoroutine(hitParticleVFX.PlayVFX());
    }
}