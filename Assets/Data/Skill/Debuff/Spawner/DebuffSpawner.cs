using UnityEngine;

public class DebuffSpawner : Spawner
{
    private static DebuffSpawner instance;
    public static DebuffSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 DebuffSpawner is allowed to exist!");

        instance = this;
    }
}