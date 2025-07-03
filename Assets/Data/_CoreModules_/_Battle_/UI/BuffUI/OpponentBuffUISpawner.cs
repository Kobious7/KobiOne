using UnityEngine;

public class OpponentBuffUISpawner : BuffUISpawner
{
    protected override void Start()
    {
        base.Start();
        buffUIList = new();
        OpponentBuffSpawner.Instance.OnBuffObjectChange += UpdateBuffUI;
        OpponentBuffSpawner.Instance.OnBuffObjectDespawn += DespawnBuffUI;
        OpponentDebuffSpawner.Instance.OnDebuffObjectChange += UpdateBuffUI;
        OpponentDebuffSpawner.Instance.OnDebuffObjectDespawn += DespawnBuffUI;
    }
}