using UnityEngine;

public class PlayerBuffSpawner : BuffSpawner
{
    private static PlayerBuffSpawner instance;
    public static PlayerBuffSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 PlayerBuffSpawner is allowed to exist!");

        instance = this;
    }
}