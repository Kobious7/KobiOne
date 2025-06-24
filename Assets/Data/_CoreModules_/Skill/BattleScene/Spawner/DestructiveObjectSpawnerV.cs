using System.Collections.Generic;
using UnityEngine;

public class DestructiveObjectSpawner : PrefabSpawner
{
    private static DestructiveObjectSpawner instance;

    public static DestructiveObjectSpawner Instance => instance;

    [SerializeField] private int tileSpawnCount;

    public int TileSpawnCount
    {
        get => tileSpawnCount;
        set => tileSpawnCount = value;
    }

    [SerializeField] private int opSpawnCount;

    public int OpSpawnCount
    {
        get => opSpawnCount;
        set => opSpawnCount = value;
    }
    
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 DestructiveObjectSpawner is allowed to exist!");

        instance = this;
    }
}