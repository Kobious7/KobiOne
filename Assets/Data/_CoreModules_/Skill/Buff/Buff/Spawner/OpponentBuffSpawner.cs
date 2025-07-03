using UnityEngine;

public class OpponentBuffSpawner : BuffSpawner
{
    private static OpponentBuffSpawner instance;
    public static OpponentBuffSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 OpponentBuffSpawner is allowed to exist!");

        instance = this;
    }
}