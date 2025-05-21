using JetBrains.Annotations;
using UnityEngine;

public class PlayerBasicSlashTrailVFX : GMono
{
    [SerializeField] private TrailRenderer trailEffect;
    [SerializeField] private RotateAroundPoint trailPointsHandler;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTrailEffect();
        LoadTrailPointsHandler();
    }

    private void LoadTrailEffect()
    {
        if (trailEffect != null) return;

        trailEffect = GetComponentInChildren<TrailRenderer>();

        trailEffect.emitting = false;
        trailEffect.gameObject.SetActive(false);
    }

    private void LoadTrailPointsHandler()
    {
        if (trailPointsHandler != null) return;

        trailPointsHandler = GetComponentInChildren<RotateAroundPoint>();
    }

    public void PlayVFX()
    {
        trailEffect.gameObject.SetActive(true);
        trailPointsHandler.StartRotation();

        trailEffect.emitting = true;
    }

    public void StopVFX()
    {
        trailEffect.gameObject?.SetActive(false);
        trailPointsHandler.ResetRotation();

        trailEffect.emitting = false;
    }
}
