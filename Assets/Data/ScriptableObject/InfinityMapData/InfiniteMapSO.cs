using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfinityMapData", menuName = "ScriptableObjects/InfiniteMapData")]
public class InfiniteMapSO : ScriptableObject
{
    [SerializeField] private bool mapCanLoad;
    [SerializeField] private Result result;
    [SerializeField] private InfiniteMapInfo mapInfo;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private MonsterInfo monsterInfo;
    [SerializeField] private List<ItemSO> itemDropList;
    [SerializeField] private List<EquipSO> equipDropList;
    [SerializeField] private Map1MonsterSpawnerInfo map1MonsterSpawnerInfo;
    [SerializeField] private Map2MonsterSpawnerInfo map2MonsterSpawnerInfo;

    #region InfiniteMapSO element getters and setters
    public bool MapCanLoad
    {
        get => mapCanLoad;
        set => mapCanLoad = value;
    }

    public Result Result
    {
        get => result;
        set => result = value;
    }

    public InfiniteMapInfo MapInfo
    {
        get => mapInfo;
        set => mapInfo = value;
    }

    public PlayerInfo PlayerInfo
    {
        get => playerInfo;
        set => playerInfo = value;
    }

    public MonsterInfo MonsterInfo
    {
        get => monsterInfo;
        set => monsterInfo = value;
    }

    public List<ItemSO> ItemDropList
    {
        get => itemDropList;
        set => itemDropList = value;
    }

    public List<EquipSO> EquipDropList
    {
        get => equipDropList;
        set => equipDropList = value;
    }

    public Map1MonsterSpawnerInfo Map1MonsterSpawnerInfo
    {
        get => map1MonsterSpawnerInfo;
        set => map1MonsterSpawnerInfo = value;
    }

    public Map2MonsterSpawnerInfo Map2MonsterSpawnerInfo
    {
        get => map2MonsterSpawnerInfo;
        set => map2MonsterSpawnerInfo = value;
    }
    #endregion

    public void ClearAllData()
    {
        mapCanLoad = false;
        result = Result.NONE;
        mapInfo = new();
        playerInfo = new();
        monsterInfo = new();
        itemDropList = new();
        equipDropList = new();
        map1MonsterSpawnerInfo = new();
        map2MonsterSpawnerInfo = new();
    }
}