using UnityEngine;

public class OpponentDebuffSpawner : DebuffSpawner
{
    private static OpponentDebuffSpawner instance;
    public static OpponentDebuffSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 OpponentDebuffSpawner is allowed to exist!");

        instance = this;
    }
}