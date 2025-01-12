using Battle;
using UnityEngine;

public abstract class DebuffObjectAb : GMono
{
    [SerializeField] private DebuffObject debuffObject;

    public DebuffObject DebuffObject => debuffObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDebuffObject();
    }

    private void LoadDebuffObject()
    {
        if(debuffObject != null) return;

        debuffObject = transform.parent.GetComponent<DebuffObject>();
    }
}