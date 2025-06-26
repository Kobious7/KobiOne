using UnityEngine;

public abstract class BEntityComponent : GMono
{
    [SerializeField] private BEntity entity;

    public BEntity Entity => entity;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEntity();
    }

    private void LoadEntity()
    {
        if (entity != null) return;

        entity = GetComponentInParent<BEntity>();
    }
}