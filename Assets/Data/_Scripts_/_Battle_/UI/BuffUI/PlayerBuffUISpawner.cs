using UnityEngine;

public class PlayerBuffUISpawner : BuffUISpawner
{
    protected override void Start()
    {
        base.Start();
        buffUIList = new();
        PlayerBuffSpawner.Instance.OnBuffObjectChange += UpdateBuffUI;
        PlayerBuffSpawner.Instance.OnBuffObjectDespawn += DespawnBuffUI;
        PlayerDebuffSpawner.Instance.OnDebuffObjectChange += UpdateBuffUI;
        PlayerDebuffSpawner.Instance.OnDebuffObjectDespawn += DespawnBuffUI;
    }
}