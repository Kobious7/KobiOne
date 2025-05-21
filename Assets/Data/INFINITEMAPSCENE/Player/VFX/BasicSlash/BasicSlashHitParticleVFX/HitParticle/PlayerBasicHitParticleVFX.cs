using System.Collections;
using UnityEngine;

public class PlayerBasicHitParticleVFX : GMono
{
    [SerializeField] private ParticleSystem hitParticle;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParticle();
    }

    private void LoadParticle()
    {
        if (hitParticle != null) return;

        hitParticle = GetComponentInChildren<ParticleSystem>();
    }

    public IEnumerator PlayVFX()
    {
        hitParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        yield return null;

        hitParticle.Play();
    }
}