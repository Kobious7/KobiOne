using UnityEngine;

public class DebuffObject : BuffInfo
{
    [SerializeField] private DebuffObjectHandling debuffHandler;

    public DebuffObjectHandling DebuffHandler => debuffHandler;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDebuffHanlder();
    }

    private void LoadDebuffHanlder()
    {
        if(debuffHandler != null) return;

        debuffHandler = GetComponentInChildren<DebuffObjectHandling>();
    }
}