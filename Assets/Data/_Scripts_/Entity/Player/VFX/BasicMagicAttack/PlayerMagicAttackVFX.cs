using UnityEngine;

public class PlayerMagicAttackVFX : GMono
{
    [SerializeField] private PlayerParticleAttractorVFX attractorVFX;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAttactorVFX();
    }

    private void LoadAttactorVFX()
    {
        if (attractorVFX != null) return;

        attractorVFX = GetComponentInChildren<PlayerParticleAttractorVFX>();
    }

    public void PlayMagicAttactor()
    {
        attractorVFX.PlayVFX();
    }
}