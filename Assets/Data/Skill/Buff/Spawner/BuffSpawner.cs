using UnityEngine;

public class BuffSpawner : Spawner
{
    private static BuffSpawner instance;
    public static BuffSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 BuffSpawner is allowed to exist!");

        instance = this;
    }
}