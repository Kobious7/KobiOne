using UnityEngine;

public class DestructiveObjectFXSpawner : PrefabSpawner
{
    private static DestructiveObjectFXSpawner instance;

    public static DestructiveObjectFXSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 DestructiveObjectFXSpawner!");

        instance = this;
    }
}