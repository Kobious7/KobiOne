using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : GMono
{
    [SerializeField] protected InfiniteMapSO mapData;

    protected override void Start()
    {
        GetMapData();
    }

    protected virtual void GetMapData() {}

    private void OnApplicationQuit()
    {
        mapData.MapCanLoad = false;
        mapData.Result = Result.NONE;
        mapData.MapInfo = new();
        mapData.PlayerInfo = new();
        mapData.MonsterInfo = new();
        mapData.ItemDropList = new();
        mapData.EquipDropList = new();
        mapData.Map1MonsterSpawnerInfo = new();
        mapData.Map2MonsterSpawnerInfo = new();
        //SavingManager.Instance.SavePlayerData();
    }
}