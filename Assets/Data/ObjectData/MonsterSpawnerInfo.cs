using System.Collections.Generic;

public abstract class MonsterSpawnerInfo
{
    public float Timer, Counter;
    public bool CanSpawn, LockSpawn;
    public int SpawnCount;
    public List<MonsterInfo> MonsterInfos;
}