using UnityEngine;

public abstract class DestructiveObjectAb : GMono
{
    [SerializeField] private DestructiveObject dObject;

    public DestructiveObject DObject => dObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDestrcutiveObject();
    }

    private void LoadDestrcutiveObject()
    {
        if(dObject != null) return;

        dObject = GetComponentInParent<DestructiveObject>();
    }
}