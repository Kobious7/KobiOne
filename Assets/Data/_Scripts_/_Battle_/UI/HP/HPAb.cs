using UnityEngine;

public abstract class HPAb : GMono
{
    [SerializeField] private HP hPAbs;

    public HP HPAbs => hPAbs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHP();
    }

    private void LoadHP()
    {
        if (hPAbs != null) return;

        hPAbs = transform.parent.GetComponent<HP>();
    }
}