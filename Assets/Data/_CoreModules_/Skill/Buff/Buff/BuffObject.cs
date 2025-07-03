using UnityEngine;

public class BuffObject : BuffInfo
{
    [SerializeField] private BuffObjectHandling buffHandler;

    public BuffObjectHandling BuffHandler => buffHandler;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBuffHanlder();
    }

    private void LoadBuffHanlder()
    {
        if(buffHandler != null) return;

        buffHandler = GetComponentInChildren<BuffObjectHandling>();
    }
}