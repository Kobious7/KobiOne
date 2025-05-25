using UnityEngine;

public class Map2MonsterSpawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
        counter = Game.Instance.MapData.Map1MonsterSpawnerInfo.Counter;
        spawnCount = 0;
        canSpawn = true;
        lockSpawn = true;

        Game.Instance.Map.MapSwap.OnMapSwap += SpawnInCurrentMap;
    }

    private void SpawnInCurrentMap(MapEnum current)
    {
        if(current == MapEnum.Map1) lockSpawn = false;
        else
        {
            lockSpawn = true;
            DespawnAllMonster();
            spawnCount = 0;
            canSpawn = true;
            counter = 0;
        }
    }
}