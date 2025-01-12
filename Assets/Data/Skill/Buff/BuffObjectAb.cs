using Battle;
using UnityEngine;

public abstract class BuffObjectAb : GMono
{
    [SerializeField] private BuffObject buffObject;

    public BuffObject BuffObject => buffObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBuffObject();
    }

    private void LoadBuffObject()
    {
        if(buffObject != null) return;

        buffObject = transform.parent.GetComponent<BuffObject>();
    }
}