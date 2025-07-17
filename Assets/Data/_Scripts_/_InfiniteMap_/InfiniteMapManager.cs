using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapManager : GMono
{
    private static InfiniteMapManager instance;

    public static InfiniteMapManager Instance => instance;

    [SerializeField] private InfiniteMapSO mapData;

    public InfiniteMapSO MapData => mapData;

    [SerializeField] private CharacterSO characterData;

    public CharacterSO CharacterData => characterData;

    [SerializeField] private FlyObjectSpawner flyObjectSpawner;

    public FlyObjectSpawner FlyObjectSpawner => flyObjectSpawner;

    [SerializeField] private IMPlayer player;

    public IMPlayer Player => player;

    [SerializeField] private Map map;

    public Map Map => map;

    [SerializeField] private Map1MonsterSpawner map1MonsterSpawner;

    public Map1MonsterSpawner Map1MonsterSpawner => map1MonsterSpawner;

    [SerializeField] private Map2MonsterSpawner map2MonsterSpawner;

    public Map2MonsterSpawner Map2MonsterSpawner => map2MonsterSpawner;

    [SerializeField] private Inventory inventory;

    public Inventory Inventory => inventory;

    [SerializeField] private Equipment equipment;

    public Equipment Equipment => equipment;

    [SerializeField] private InfinitMapSODataLoader infinitMapSODataLoader;

    public InfinitMapSODataLoader InfinitMapSODataLoader => infinitMapSODataLoader;

    [SerializeField] private Skill skill;

    public Skill Skill => skill;

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
        LoadCharacterData();
        LoadPlayer();
        LoadFlyObjectSpawner();
        LoadMap();
        LoadMonsterSpawners();
        LoadInventory();
        LoadEquipment();
        LoadSODataLoader();
        
        if (skill == null) skill = FindObjectOfType<Skill>();
    }

    protected void LoadMapData()
    {
        if (mapData != null) return;

        mapData = Resources.Load<InfiniteMapSO>("InfinityMapData");
    }

    private void LoadCharacterData()
    {
        if (characterData != null) return;

        characterData = Resources.Load<CharacterSO>("Character/Character");
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

    public void LoadDataToInfiniteMapSO(Transform monster)
    {
        infinitMapSODataLoader.LoadAllObj(monster);
    }
}