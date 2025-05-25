using UnityEngine;

public class FlyObject : GMono
{
    [SerializeField] protected Transform model;

    public Transform Model => model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
    }

    protected void LoadModel()
    {
        if (model != null) return;

        model = transform.Find("Model");
    }
}