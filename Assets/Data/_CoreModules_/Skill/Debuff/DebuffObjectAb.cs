using UnityEngine;

public abstract class DebuffObjectAb : GMono
{
    [SerializeField] private DebuffObject debuffObj;

    public DebuffObject DebuffObj => debuffObj;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDebuffObject();
    }

    private void LoadDebuffObject()
    {
        if(debuffObj != null) return;

        debuffObj = transform.parent.GetComponent<DebuffObject>();
    }
}