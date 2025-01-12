using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfinityMapData", menuName = "ScriptableObjects/InfiniteMapData")]
public class InfiniteMapSO : ScriptableObject
{
    [SerializeField] private bool mapCanLoad;

    public bool MapCanLoad
    {
        get => mapCanLoad;
        set => mapCanLoad = value;
    }

    [SerializeField] private Result result;

    public Result Result
    {
        get => result;
        set => result = value;
    }

    [SerializeField] private InfiniteMapInfo infiniteMapInfo;

    public InfiniteMapInfo InfiniteMapInfo
    {
        get => infiniteMapInfo;
        set => infiniteMapInfo = value;
    }

    [SerializeField] private PlayerInfo playerInfo;

    public PlayerInfo PlayerInfo
    {
        get => playerInfo;
        set => playerInfo = value;
    }

    [SerializeField] private MonsterInfo monsterInfo;

    public MonsterInfo MonsterInfo
    {
        get => monsterInfo;
        set => monsterInfo = value;
    }

    [SerializeField] private List<ItemSO> itemDropList;

    public List<ItemSO> ItemDropList
    {
        get => itemDropList;
        set => itemDropList = value;
    }

    [SerializeField] private List<EquipSO> equipDropList;

    public List<EquipSO> EquipDropList
    {
        get => equipDropList;
        set => equipDropList = value;
    }

    [SerializeField] private Map1MonsterSpawnerInfo map1MonsterSpawnerInfo;

    public Map1MonsterSpawnerInfo Map1MonsterSpawnerInfo
    {
        get => map1MonsterSpawnerInfo;
        set => map1MonsterSpawnerInfo = value;
    }

    [SerializeField] private Map2MonsterSpawnerInfo map2MonsterSpawnerInfo;

    public Map2MonsterSpawnerInfo Map2MonsterSpawnerInfo
    {
        get => map2MonsterSpawnerInfo;
        set =>map2MonsterSpawnerInfo = value;
    }

    [SerializeField] private List<InventoryItem> listItems;

    public List<InventoryItem> ListItems
    {
        get => listItems;
        set => listItems = value;
    }

    [SerializeField] private List<InventoryEquip> weaponList;

    public List<InventoryEquip> WeaponList
    {
        get => weaponList;
        set => weaponList = value;
    }

    [SerializeField] private List<InventoryEquip> helmetList;

    public List<InventoryEquip> HelmetList
    {
        get => helmetList;
        set => helmetList = value;
    }

    [SerializeField] private List<InventoryEquip> bodyArmorList;

    public List<InventoryEquip> BodyArmorList
    {
        get => bodyArmorList;
        set => bodyArmorList = value;
    }

    [SerializeField] private List<InventoryEquip> legArmorList;

    public List<InventoryEquip> LegArmorList
    {
        get => legArmorList;
        set => legArmorList = value;
    }

    [SerializeField] private List<InventoryEquip> bootsList;

    public List<InventoryEquip> BootsList
    {
        get => bootsList;
        set => bootsList = value;
    }

    [SerializeField] private List<InventoryEquip> auraList;

    public List<InventoryEquip> AuraList
    {
        get => auraList;
        set => auraList = value;
    }

    [SerializeField] private List<InventoryEquip> backItemList;

    public List<InventoryEquip> BackItemList
    {
        get => backItemList;
        set => backItemList = value;
    }
}