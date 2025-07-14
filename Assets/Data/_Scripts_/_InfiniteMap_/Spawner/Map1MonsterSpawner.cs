using System.Collections.Generic;
using UnityEngine;

public class Map1MonsterSpawner : MonsterSpawner
{
    private InfiniteMapManager infiniteMapManager;

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;

        if (infiniteMapManager.MapData.MapCanLoad)
        {
            counter = infiniteMapManager.MapData.Map1MonsterSpawnerInfo.Counter;
            spawnCount = 0;
            canSpawn = false;
            lockSpawn = false;

            LoadFromData();
        }

        infiniteMapManager.Map.MapSwap.OnMapSwap += SpawnInCurrentMap;
    }

    private void LoadFromData()
    {
        List<MonsterInfo> list = infiniteMapManager.MapData.Map1MonsterSpawnerInfo.MonsterInfos.Count > 0 ? infiniteMapManager.MapData.Map1MonsterSpawnerInfo.MonsterInfos : infiniteMapManager.MapData.Map2MonsterSpawnerInfo.MonsterInfos;

        foreach (MonsterInfo monsterInfo in list)
        {
            Transform newMonster = Spawn(prefabs[Random.Range(0, prefabs.Count)], monsterInfo.PosOffset + infiniteMapManager.Map.Maps[0].position, Quaternion.identity);
            
            IMMonster monsterCom = newMonster.GetComponent<IMMonster>();
            monsterCom.Stats.ZeroLevel = monsterInfo.ZeroLevel;
            monsterCom.Stats.AttackType = monsterInfo.AttackType;
            
            monsterCom.Stats.LoadFromData = true;
            
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
