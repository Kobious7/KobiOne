using UnityEngine;

public class SkillSpawner : Spawner
{
    [SerializeField] private int spawnedCount;

    public int SpawnedCount
    {
        get => spawnedCount;
        set => spawnedCount = value;
    }
}