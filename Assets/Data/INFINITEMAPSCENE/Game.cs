using System.Collections.Generic;
using UnityEngine;

namespace InfiniteMap
{
    public class Game : GMono
    {
        private static Game instance;

        public static Game Instance => instance;

        [SerializeField] private InfiniteMapSO mapData;

        public InfiniteMapSO MapData => mapData;

        [SerializeField] private CharacterSO characterData;

        public CharacterSO CharacterData => characterData;

        [SerializeField] private FlyObjectSpawner flyObjectSpawner;

        public FlyObjectSpawner FlyObjectSpawner => flyObjectSpawner;

        [SerializeField] private Player player;

        public Player Player => player;

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

        protected override void Awake()
        {
            base.Awake();
            if (instance != null) Debug.LogError("Only 1 Game is allowed to exist!");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadMapData();
            LoadCharacterData();
            LoadFlyObjectSpawner();
            LoadPlayer();
            LoadMap();
            LoadMonsterSpawners();
            LoadInventory();
            LoadEquipment();
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

            player = FindObjectOfType<Player>();
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
            if(inventory != null) return;

            inventory = FindObjectOfType<Inventory>();
        }

        private void LoadEquipment()
        {
            if(equipment != null) return;
            
            equipment = FindObjectOfType<Equipment>();
        }

        public void LoadAllObj(Transform monster)
        {
            mapData.MapCanLoad = true;
            LoadMapObj();
            LoadPlayerObj();
            LoadMonsterObj(monster.GetComponent<Monster>());
            LoadMap1MonsterSpawner(monster);
            LoadMap2MonsterSpawner(monster);
            LoadListItems();
            LoadEquipList();
        }

        public void LoadMapObj()
        {
            mapData.InfiniteMapInfo.Distance = map.Distance;
        }

        public void LoadPlayerObj()
        {
            Vector3 mapPos = map.MapSwap.CurrentMap == MapEnum.Map0 ? to2DVec(map.Maps[0].position) : to2DVec(map.Maps[1].position);
            Vector3 playerPos = to2DVec(player.transform.position);
            mapData.PlayerInfo.Level = player.Stats.Level;
            mapData.PlayerInfo.PosOffset = playerPos - mapPos;
            mapData.PlayerInfo.Attack = player.Stats.Stats[1].Value;
            mapData.PlayerInfo.CurrentExp = player.Stats.CurrentExp;
            mapData.PlayerInfo.MagicAttack = player.Stats.Stats[2].Value;
            mapData.PlayerInfo.HP = player.Stats.Stats[3].Value;
            mapData.PlayerInfo.SlashDamage = player.Stats.Stats[4].Value;
            mapData.PlayerInfo.SwordrainDamage = player.Stats.Stats[5].Value;
            mapData.PlayerInfo.Defense = player.Stats.Stats[6].Value;
            mapData.PlayerInfo.Accuracy = player.Stats.Stats[7].Value;
            mapData.PlayerInfo.DamageRange = player.Stats.Stats[8].PercentBonus;
            mapData.PlayerInfo.CritRate = player.Stats.Stats[10].PercentBonus;
            mapData.PlayerInfo.CritDamage = player.Stats.Stats[11].PercentBonus;
            mapData.PlayerInfo.ManaRegen = player.Stats.Stats[12].PercentBonus;
            mapData.PlayerInfo.QSkill = SkillSGT.Instance.Skill.QSkill;
            mapData.PlayerInfo.ESkill = SkillSGT.Instance.Skill.ESkill;
            mapData.PlayerInfo.SpaceSkill = SkillSGT.Instance.Skill.SpaceSkill;
        }

        public void LoadMonsterObj(Monster monster)
        {
            mapData.MonsterInfo.Level = monster.Stats.Level;
            mapData.MonsterInfo.HP = monster.Stats.MaxHP;
            mapData.MonsterInfo.SwordrainDamage = monster.Stats.SwordrainDamage;
            mapData.MonsterInfo.SlashDamage = monster.Stats.SlashDamage;
            mapData.ItemDropList = monster.DropItemList.ItemDropList;
            mapData.EquipDropList = monster.DropItemList.EquipDropList;
        }

        public void LoadMap1MonsterSpawner(Transform other)
        {
            if (map.MapSwap.CurrentMap != MapEnum.Map0) return;

            mapData.Map1MonsterSpawnerInfo.Timer = map1MonsterSpawner.Timer;
            mapData.Map1MonsterSpawnerInfo.Counter = map1MonsterSpawner.Counter;
            mapData.Map1MonsterSpawnerInfo.CanSpawn = map1MonsterSpawner.CanSpawn;
            mapData.Map1MonsterSpawnerInfo.LockSpawn = map1MonsterSpawner.LockSpawn;
            mapData.Map1MonsterSpawnerInfo.MonsterInfos = new();

            List<Transform> monsters = map1MonsterSpawner.GetActiveMonster();

            foreach (Transform monster in monsters)
            {
                if (monster == other) continue;

                Monster mons = monster.GetComponent<Monster>();
                MonsterInfo monsterInfo = new MonsterInfo();
                Vector3 posOffset = monster.position - map.Maps[0].position;
                monsterInfo.PosOffset = posOffset;
                monsterInfo.Level = mons.Stats.Level;
                monsterInfo.SlashDamage = mons.Stats.SlashDamage;
                monsterInfo.SwordrainDamage = mons.Stats.SwordrainDamage;
                
                mapData.Map1MonsterSpawnerInfo.MonsterInfos.Add(monsterInfo);
            }
        }

        public void LoadMap2MonsterSpawner(Transform other)
        {
            mapData.Map2MonsterSpawnerInfo.Timer = map2MonsterSpawner.Timer;
            mapData.Map2MonsterSpawnerInfo.Counter = map2MonsterSpawner.Counter;
            mapData.Map2MonsterSpawnerInfo.CanSpawn = map2MonsterSpawner.CanSpawn;
            mapData.Map2MonsterSpawnerInfo.LockSpawn = map2MonsterSpawner.LockSpawn;
            mapData.Map2MonsterSpawnerInfo.MonsterInfos = new();

            List<Transform> monsters = map2MonsterSpawner.GetActiveMonster();

            foreach (Transform monster in monsters)
            {
                if (monster == other) continue;

                Monster mons = monster.GetComponent<Monster>();
                MonsterInfo monsterInfo = new MonsterInfo();
                Vector3 posOffset = monster.position - map.Maps[1].position;
                monsterInfo.PosOffset = posOffset;
                monsterInfo.Level = mons.Stats.Level;
                monsterInfo.SlashDamage = mons.Stats.SlashDamage;
                monsterInfo.SwordrainDamage = mons.Stats.SwordrainDamage;

                mapData.Map2MonsterSpawnerInfo.MonsterInfos.Add(monsterInfo);
            }
        }

        private void LoadListItems()
        {
            mapData.ListItems = inventory.ListItems;
        }

        private void LoadEquipList()
        {
            mapData.WeaponList = inventory.WeaponList;
            mapData.HelmetList = inventory.HelmetList;
            mapData.BodyArmorList = inventory.BodyArmorList;
            mapData.LegArmorList = inventory.LegArmorList;
            mapData.BootsList = inventory.BootsList;
            mapData.AuraList = inventory.AuraList;
            mapData.BackItemList = inventory.BackItemList;
        }
    }
}