using UnityEngine;

public class PlayerSwordA1SHealVFX : GMono
{
    [SerializeField] private ParticleSystem healParticle;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadParticle();
    }

    private void LoadParticle()
    {
        if (healParticle != null) return;

        healParticle = GetComponent<ParticleSystem>();
    }

    public void PlayHealParticle()
    {
        healParticle.Play();
    }
}