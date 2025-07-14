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
    [SerializeField] private List<InventoryEquip> armorList;
    [SerializeField] private List<InventoryEquip> armwearList;
    [SerializeField] private List<InventoryEquip> bootsList;
    [SerializeField] private List<InventoryEquip> specialList;
    [SerializeField] private InventoryEquip weapon;
    [SerializeField] private InventoryEquip helmet;
    [SerializeField] private InventoryEquip armor;
    [SerializeField] private InventoryEquip armwear;
    [SerializeField] private InventoryEquip boots;
    [SerializeField] private InventoryEquip special;

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

    public List<InventoryEquip> ArmorList
    {
        get => armorList;
        set => armorList = value;
    }

    public List<InventoryEquip> ArmwearList
    {
        get => armwearList;
        set => armwearList = value;
    }

    public List<InventoryEquip> BootsList
    {
        get => bootsList;
        set => bootsList = value;
    }

    public List<InventoryEquip> SpecialList
    {
        get => specialList;
        set => specialList = value;
    }

    public InventoryEquip Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    public InventoryEquip Helmet
    {
        get => helmet;
        set => helmet = value;
    }

    public InventoryEquip Armor
    {
        get => armor;
        set => armor = value;
    }

    public InventoryEquip Armwear
    {
        get => armwear;
        set => armwear = value;
    }

    public InventoryEquip Boots
    {
        get => boots;
        set => boots = value;
    }

    public InventoryEquip Special
    {
        get => special;
        set => special = value;
    }
    #endregion
}