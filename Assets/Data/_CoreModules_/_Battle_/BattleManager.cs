using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : GMono
{
    private static BattleManager instance;

    public static BattleManager Instance => instance;

    [SerializeField] private InfiniteMapSO mapData;

    public InfiniteMapSO MapData => mapData;

    [SerializeField] private CharacterSO characterData;

    public CharacterSO CharacterData => characterData;

    [SerializeField] private MonsterObjectCreator monsterObjectCreator;

    public MonsterObjectCreator MonsterObjectCreator => monsterObjectCreator;

    [SerializeField] private FlyObjectSpawner flyObjectSpawner;

    public FlyObjectSpawner FlyObjectSpawner => flyObjectSpawner;

    [SerializeField] private BPlayer player;

    public BPlayer Player => player;

    [SerializeField] private BMonster monster;

    public BMonster Monster => monster;

    [SerializeField] private TileBackgroundSpawner tileBackgroundSpawner;

    public TileBackgroundSpawner TileBackgroundSpawner => tileBackgroundSpawner;

    [SerializeField] private TileSpawner tileSpawner;

    public TileSpawner TileSpawner => tileSpawner;

    [SerializeField] private Board board;

    public Board Board => board;

    [SerializeField] private SwordrainSpawner swordrainSpawner;

    public SwordrainSpawner SwordrainSpawner => swordrainSpawner;

    [SerializeField] private BPlayerSpawnPointContainer playerSpawnPointContainer;

    public BPlayerSpawnPointContainer PlayerSpawnPointContainer => playerSpawnPointContainer;

    [SerializeField] private BMonsterSpawnPointContainer monsterSpawnPointContainer;

    public BMonsterSpawnPointContainer MonsterSpawnPointContainer => monsterSpawnPointContainer;

    [SerializeField] private FXSpawner fxSpawner;

    public FXSpawner FXSpawner => fxSpawner;

    [SerializeField] private TileBorder tileBorder;

    public TileBorder TileBorder => tileBorder;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 BattleManager is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMapData();
        LoadCharacterData();
        LoadMonsterObjectCreator();
        monsterObjectCreator.SpawnMonsterObject("Ellipsy");
        LoadPlayer();
        LoadFlyObjectSpawner();
        LoadTileBGSpawner();
        LoadTileSpawner();
        LoadBoard();
        LoadMonster();
        LoadSwordrainSpawner();
        LoadPlayerSpawnPointConatiner();
        LoadBotSpawnPointConatiner();
        LoadFXSpawner();
        LoadTileBorder();
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

    private void LoadMonsterObjectCreator()
    {
        if (monsterObjectCreator != null) return;

        monsterObjectCreator = FindObjectOfType<MonsterObjectCreator>();
    }

    private void LoadFlyObjectSpawner()
    {
        if (flyObjectSpawner != null) return;

        flyObjectSpawner = FindObjectOfType<FlyObjectSpawner>();
    }

    private void LoadPlayer()
    {
        if (player != null) return;

        player = FindObjectOfType<BPlayer>();
    }

    private void LoadMonster()
    {
        if (monster != null) return;

        monster = FindObjectOfType<BMonster>();
    }

    private void LoadTileBGSpawner()
    {
        if (tileBackgroundSpawner != null) return;

        tileBackgroundSpawner = FindObjectOfType<TileBackgroundSpawner>();
    }

    private void LoadTileSpawner()
    {
        if (tileSpawner != null) return;

        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    private void LoadBoard()
    {
        if (board != null) return;

        board = FindObjectOfType<Board>();
    }

    private void LoadSwordrainSpawner()
    {
        if (swordrainSpawner != null) return;

        swordrainSpawner = FindObjectOfType<SwordrainSpawner>();
    }

    private void LoadPlayerSpawnPointConatiner()
    {
        if (playerSpawnPointContainer != null) return;

        playerSpawnPointContainer = FindObjectOfType<BPlayerSpawnPointContainer>();
    }

    private void LoadBotSpawnPointConatiner()
    {
        if (monsterSpawnPointContainer != null) return;

        monsterSpawnPointContainer = FindObjectOfType<BMonsterSpawnPointContainer>();
    }

    private void LoadFXSpawner()
    {
        if (fxSpawner != null) return;

        fxSpawner = FindObjectOfType<FXSpawner>();
    }

    private void LoadTileBorder()
    {
        if (tileBorder != null) return;

        tileBorder = FindObjectOfType<TileBorder>();
    }
}