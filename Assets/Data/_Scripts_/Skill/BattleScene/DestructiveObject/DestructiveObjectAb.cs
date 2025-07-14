using UnityEngine;

public abstract class DestructiveObjectAb : GMono
{
    [SerializeField] private DestructiveObject dObject;

    public DestructiveObject DObject => dObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDestructiveObject();
    }

    private void LoadDestructiveObject()
    {
        if(dObject != null) return;

        dObject = GetComponentInParent<DestructiveObject>();
    }
}