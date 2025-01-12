using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class Game : GMono
    {
        private static Game instance;

        public static Game Instance => instance;

        [SerializeField] private InfiniteMapSO mapData;

        public InfiniteMapSO MapData => mapData;


        [SerializeField] private TileBackgroundSpawner tileBackgroundSpawner;

        public TileBackgroundSpawner TileBackgroundSpawner => tileBackgroundSpawner;

        [SerializeField] private TileSpawner tileSpawner;

        public TileSpawner TileSpawner => tileSpawner;

        [SerializeField] private Board board;

        public Board Board => board;

        [SerializeField] private Bot bot;

        public Bot Bot => bot;

        [SerializeField] private Player player;

        public Player Player => player;

        [SerializeField] private SwordrainSpawner swordrainSpawner;

        public SwordrainSpawner SwordrainSpawner => swordrainSpawner;

        [SerializeField] private PlayerSpawnPointContainer playerSpawnPointContainer;

        public PlayerSpawnPointContainer PlayerSpawnPointContainer => playerSpawnPointContainer;

        [SerializeField] private BotSpawnPointContainer botSpawnPointContainer;

        public BotSpawnPointContainer BotSpawnPointContainer => botSpawnPointContainer;

        [SerializeField] private SkillSpawner skillSpawner;

        public SkillSpawner SkillSpawner => skillSpawner;

        [SerializeField] private List<Skill> playerSkill;

        public List<Skill> PlayerSkill => playerSkill;

        [SerializeField] private FXSpawner fxSpawner;

        public FXSpawner FXSpawner => fxSpawner;

        [SerializeField] private TileBorder tileBorder;

        public TileBorder TileBorder => tileBorder;

        protected override void Awake()
        {
            base.Awake();
            if (instance != null) Debug.LogError("Only 1 Game instacne is allowed to exist!");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadMapData();
            LoadTileBGSpawner();
            LoadTileSpawner();
            LoadBoard();
            LoadBot();
            LoadPlayer();
            LoadSwordrainSpawner();
            LoadPlayerSpawnPointConatiner();
            LoadBotSpawnPointConatiner();
            LoadSkillSpawner();
            LoadPlayerSkill();
            LoadFXSpawner();
            LoadTileBorder();
        }

        private void LoadMapData()
        {
            if (mapData != null) return;

            mapData = Resources.Load<InfiniteMapSO>("InfinityMapData");
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

        private void LoadBot()
        {
            if (bot != null) return;

            bot = FindObjectOfType<Bot>();
        }

        private void LoadPlayer()
        {
            if (player != null) return;

            player = FindObjectOfType<Player>();
        }

        private void LoadSwordrainSpawner()
        {
            if (swordrainSpawner != null) return;

            swordrainSpawner = FindObjectOfType<SwordrainSpawner>();
        }

        private void LoadPlayerSpawnPointConatiner()
        {
            if (playerSpawnPointContainer != null) return;

            playerSpawnPointContainer = FindObjectOfType<PlayerSpawnPointContainer>();
        }

        private void LoadBotSpawnPointConatiner()
        {
            if (botSpawnPointContainer != null) return;

            botSpawnPointContainer = FindObjectOfType<BotSpawnPointContainer>();
        }

        private void LoadSkillSpawner()
        {
            if (skillSpawner != null) return;

            skillSpawner = FindObjectOfType<SkillSpawner>();
        }

        private void LoadPlayerSkill()
        {
            if (playerSkill.Count > 0) return;

            playerSkill = FindObjectOfType<PlayerSkillContainer>().GetComponentsInChildren<Skill>().ToList();
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
}