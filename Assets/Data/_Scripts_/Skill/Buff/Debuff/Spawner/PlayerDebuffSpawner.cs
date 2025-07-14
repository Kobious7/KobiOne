using UnityEngine;

public class PlayerDebuffSpawner : DebuffSpawner
{
    private static PlayerDebuffSpawner instance;
    public static PlayerDebuffSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 PlayerDebuffSpawner is allowed to exist!");

        instance = this;
    }
}