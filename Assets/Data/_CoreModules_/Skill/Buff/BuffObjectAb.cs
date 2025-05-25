using UnityEngine;

public abstract class BuffObjectAb : GMono
{
    [SerializeField] private BuffObject buffObj;

    public BuffObject BuffObj => buffObj;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBuffObject();
    }

    private void LoadBuffObject()
    {
        if(buffObj != null) return;

        buffObj = transform.parent.GetComponent<BuffObject>();
    }
}