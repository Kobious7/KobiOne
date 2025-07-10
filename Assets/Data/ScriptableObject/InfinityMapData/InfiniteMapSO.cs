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
    [SerializeField] private int primarionSoul;
    [SerializeField] private List<ItemSO> itemDropList;
    [SerializeField] private List<EquipSO> equipDropList;
    [SerializeField] private Map1MonsterSpawnerInfo map1MonsterSpawnerInfo;
    [SerializeField] private Map2MonsterSpawnerInfo map2MonsterSpawnerInfo;
    [SerializeField] private List<InventoryItem> itemList;
    [SerializeField] private List<InventoryEquip> weaponList;
    [SerializeField] private List<InventoryEquip> helmetList;
    [SerializeField] private List<InventoryEquip> bodyArmorList;
    [SerializeField] private List<InventoryEquip> legArmorList;
    [SerializeField] private List<InventoryEquip> bootsList;
    [SerializeField] private List<InventoryEquip> auraList;
    [SerializeField] private List<InventoryEquip> backItemList;

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

    public int PrimarionSoul
    {
        get => primarionSoul;
        set => primarionSoul = value;
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

    public List<InventoryItem> ItemList
    {
        get => itemList;
        set => itemList = value;
    }

    public List<InventoryEquip> WeaponList
    {
        get => weaponList;
        set => weaponList = value;
    }

    public List<InventoryEquip> HelmetList
    {
        get => helmetList;
        set => helmetList = value;
    }

    public List<InventoryEquip> BodyArmorList
    {
        get => bodyArmorList;
        set => bodyArmorList = value;
    }

    public List<InventoryEquip> LegArmorList
    {
        get => legArmorList;
        set => legArmorList = value;
    }

    public List<InventoryEquip> BootsList
    {
        get => bootsList;
        set => bootsList = value;
    }

    public List<InventoryEquip> AuraList
    {
        get => auraList;
        set => auraList = value;
    }

    public List<InventoryEquip> BackItemList
    {
        get => backItemList;
        set => backItemList = value;
    }
    #endregion
}