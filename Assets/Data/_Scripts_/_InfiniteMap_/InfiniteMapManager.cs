using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapManager : GMono
{
    private static InfiniteMapManager instance;

    public static InfiniteMapManager Instance => instance;

    [SerializeField] private bool isUIOpening = false;
    [SerializeField] private InfiniteMapSO mapData;
    [SerializeField] private PlayerSO playerData;
    [SerializeField] private FlyObjectSpawner flyObjectSpawner;
    [SerializeField] private IMPlayer player;
    [SerializeField] private Map map;
    [SerializeField] private Map1MonsterSpawner map1MonsterSpawner;
    [SerializeField] private Map2MonsterSpawner map2MonsterSpawner;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Equipment equipment;
    [SerializeField] private InfinitMapSODataLoader infinitMapSODataLoader;
    [SerializeField] private Skill skill;
    [SerializeField] private BattleEntrance battleEntrance;

    #region Properties
    public bool IsUIOpening { get => isUIOpening; set => isUIOpening = value; }
    public InfiniteMapSO MapData => mapData;
    public PlayerSO PlayerData => playerData;
    public FlyObjectSpawner FlyObjectSpawner => flyObjectSpawner;
    public IMPlayer Player => player;
    public Map Map => map;
    public Map1MonsterSpawner Map1MonsterSpawner => map1MonsterSpawner;
    public Map2MonsterSpawner Map2MonsterSpawner => map2MonsterSpawner;
    public Inventory Inventory => inventory;
    public Equipment Equipment => equipment;
    public InfinitMapSODataLoader InfinitMapSODataLoader => infinitMapSODataLoader;
    public Skill Skill => skill;
    public BattleEntrance BattleEntrance => battleEntrance;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 InfiniteMapManager is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMapData();
        LoadPlayerData();
        LoadPlayer();
        LoadFlyObjectSpawner();
        LoadMap();
        LoadMonsterSpawners();
        LoadInventory();
        LoadEquipment();
        LoadSODataLoader();

        if (skill == null) skill = FindObjectOfType<Skill>();
        if (battleEntrance == null) battleEntrance = FindObjectOfType<BattleEntrance>();
    }

    protected void LoadMapData()
    {
        if (mapData != null) return;

        mapData = Resources.Load<InfiniteMapSO>("InfinityMapData");
    }

    private void LoadPlayerData()
    {
        if (playerData != null) return;

        playerData = Resources.Load<PlayerSO>("Player/Player");
    }

    private void LoadFlyObjectSpawner()
    {
        if (flyObjectSpawner != null) return;

        flyObjectSpawner = FindObjectOfType<FlyObjectSpawner>();
    }

    private void LoadPlayer()
    {
        if (player != null) return;

        player = FindObjectOfType<IMPlayer>();
    }

    private void LoadMap()
    {
        if (map != null) return;

        map = FindObjectOfType<Map>();
    }

    private void LoadMonsterSpawners()
    {
        if (map1MonsterSpawner != null && map2MonsterSpawner != null) return;

        map1MonsterSpawner = FindObjectOfType<Map1MonsterSpawner>();
        map2MonsterSpawner = FindObjectOfType<Map2MonsterSpawner>();
    }

    private void LoadInventory()
    {
        if (inventory != null) return;

        inventory = FindObjectOfType<Inventory>();
    }

    private void LoadEquipment()
    {
        if (equipment != null) return;

        equipment = FindObjectOfType<Equipment>();
    }

    private void LoadSODataLoader()
    {
        if (infinitMapSODataLoader != null) return;

        infinitMapSODataLoader = FindObjectOfType<InfinitMapSODataLoader>();
    }
}