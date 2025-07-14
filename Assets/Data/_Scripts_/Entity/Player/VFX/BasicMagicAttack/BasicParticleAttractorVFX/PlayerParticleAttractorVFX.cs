using UnityEngine;

public class PlayerParticleAttractorVFX : GMono
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleAttractor attractor;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParticle();
        LoadAttactor();
    }

    private void LoadParticle()
    {
        if (particle != null) return;

        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void LoadAttactor()
    {
        if (attractor != null) return;

        attractor = GetComponentInChildren<ParticleAttractor>();

        attractor.gameObject.SetActive(false);
    }

    public void PlayVFX()
    {
        particle.Play();
        attractor.gameObject.SetActive(true);
    }
}