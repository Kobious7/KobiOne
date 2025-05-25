using UnityEngine;

public abstract class BattleFXAb : GMono
{
    [SerializeField] private BattleFX fx;

    public BattleFX FX => fx;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadFX();
    }

    private void LoadFX()
    {
        if (fx != null) return;

        fx = transform.parent.GetComponent<BattleFX>();
    }
}