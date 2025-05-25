using System.Collections.Generic;
using UnityEngine;

public class Map1MonsterSpawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();

        if(Game.Instance.MapData.MapCanLoad)
        {
            counter = Game.Instance.MapData.Map1MonsterSpawnerInfo.Counter;
            spawnCount = 0;
            canSpawn = false;
            lockSpawn = false;

            LoadFromData();
        }

        Game.Instance.Map.MapSwap.OnMapSwap += SpawnInCurrentMap;
    }

    private void LoadFromData()
    {
        List<MonsterInfo> list = Game.Instance.MapData.Map1MonsterSpawnerInfo.MonsterInfos.Count > 0 ? Game.Instance.MapData.Map1MonsterSpawnerInfo.MonsterInfos : Game.Instance.MapData.Map2MonsterSpawnerInfo.MonsterInfos;

        foreach (MonsterInfo monsterInfo in list)
        {
            Transform newMonster = Spawn(prefabs[Random.Range(0, prefabs.Count)], monsterInfo.PosOffset + Game.Instance.Map.Maps[0].position, Quaternion.identity);
            
            newMonster.gameObject.SetActive(true);

            spawnCount++;
        }
    }

    private void SpawnInCurrentMap(MapEnum current)
    {
        if (current == MapEnum.Map0) lockSpawn = false;
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
