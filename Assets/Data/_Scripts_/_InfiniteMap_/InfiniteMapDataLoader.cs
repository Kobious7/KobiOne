using System.Collections.Generic;
using UnityEngine;

public class InfinitMapSODataLoader : GMono
{
    private InfiniteMapSO mapData;
    private IMPlayer player;
    private Map map;
    private Map1MonsterSpawner map1MonsterSpawner;
    private Map2MonsterSpawner map2MonsterSpawner;
    private Inventory inventory;
    private Equipment equipment;
    private Skill skill;

    protected override void Start()
    {
        InfiniteMapManager infiniteMapManager = InfiniteMapManager.Instance;
        mapData = infiniteMapManager.MapData;
        player = infiniteMapManager.Player;
        map = infiniteMapManager.Map;
        map1MonsterSpawner = infiniteMapManager.Map1MonsterSpawner;
        map2MonsterSpawner = infiniteMapManager.Map2MonsterSpawner;
        inventory = infiniteMapManager.Inventory;
        equipment = infiniteMapManager.Equipment;
        skill = infiniteMapManager.Skill;
        player.OnBattlePreparing += LoadDataToInfiniteMapSO;
    }

    public void LoadDataToInfiniteMapSO(IMMonster monster)
    {
        LoadAllObj(monster);
    }

    public void LoadAllObj(IMMonster monster)
    {
        mapData.MapCanLoad = true;
        LoadMapObj();
        LoadPlayerObj();
        LoadMonsterObj(monster);
        LoadMap1MonsterSpawner(monster);
        LoadMap2MonsterSpawner(monster);
    }


    public void LoadMapObj()
    {
        mapData.MapInfo.Distance = map.Distance;
        mapData.MapInfo.Level = map.MapLevel.CurrentLevel;
        mapData.MapInfo.ReloadMonster = true;
    }

    public void LoadPlayerObj()
    {
        Vector3 mapPos = map.MapSwap.CurrentMap == MapEnum.Map0 ? to2DVec(map.Maps[0].position) : to2DVec(map.Maps[1].position);
        Vector3 playerPos = to2DVec(player.transform.position);
        mapData.PlayerInfo.Level = player.StatsSystem.Level;
        mapData.PlayerInfo.CurrentExp = player.StatsSystem.CurrentExp;
        mapData.PlayerInfo.RequiredExp = player.StatsSystem.RequiredExp;
        mapData.PlayerInfo.ExpFromBattle = 0;
        mapData.PlayerInfo.PosOffset = playerPos - mapPos;
        mapData.PlayerInfo.Attack = player.StatsSystem.Stats[1].Value;
        mapData.PlayerInfo.MagicAttack = player.StatsSystem.Stats[2].Value;
        mapData.PlayerInfo.HP = player.StatsSystem.Stats[3].Value;
        mapData.PlayerInfo.SlashDamage = player.StatsSystem.Stats[4].Value;
        mapData.PlayerInfo.SwordrainDamage = player.StatsSystem.Stats[5].Value;
        mapData.PlayerInfo.Defense = player.StatsSystem.Stats[6].Value;
        mapData.PlayerInfo.Accuracy = player.StatsSystem.Stats[7].Value;
        mapData.PlayerInfo.DamageRange = player.StatsSystem.Stats[8].PercentBonus;
        mapData.PlayerInfo.CritRate = player.StatsSystem.Stats[10].PercentBonus;
        mapData.PlayerInfo.CritDamage = player.StatsSystem.Stats[11].PercentBonus;
        mapData.PlayerInfo.ManaRegen = player.StatsSystem.Stats[12].PercentBonus;
        mapData.PlayerInfo.ExpBonus = player.StatsSystem.Stats[13].Value;

        if (skill.QSkill != null && skill.SkillTreeList[skill.QSkill.TreeIndex].IsActive)
        {
            mapData.PlayerInfo.QSkill = skill.QSkill;
        }
        if (skill.ESkill != null && skill.SkillTreeList[skill.ESkill.TreeIndex].IsActive)
        {
            mapData.PlayerInfo.ESkill = skill.ESkill;
        }
        if (skill.SpaceSkill != null && skill.SkillTreeList[skill.SpaceSkill.TreeIndex].IsActive)
        {
            mapData.PlayerInfo.SpaceSkill = skill.SpaceSkill;
        }
    }

    public void LoadMonsterObj(IMMonster monster)
    {
        monster.Stats.CalculateStats();
        mapData.MonsterInfo.Name = monster.name;
        mapData.MonsterInfo.Level = monster.Stats.Level;
        mapData.MonsterInfo.Tier = monster.Stats.Tier;
        mapData.MonsterInfo.AttackType = monster.Stats.AttackType;
        mapData.MonsterInfo.Attack = monster.Stats.Attack;
        mapData.MonsterInfo.MagicAttack = monster.Stats.MagicAttack;
        mapData.MonsterInfo.HP = monster.Stats.HP;
        mapData.MonsterInfo.SlashDamage = monster.Stats.SlashDamage;
        mapData.MonsterInfo.SwordrainDamage = monster.Stats.SwordrainDamage;
        mapData.MonsterInfo.Defense = monster.Stats.DefenseS;
        mapData.MonsterInfo.Accuracy = monster.Stats.Accuracy;
        mapData.MonsterInfo.DamageRange = monster.Stats.DamageRange;
        mapData.MonsterInfo.CritRate = monster.Stats.CritRate;
        mapData.MonsterInfo.CritDamage = monster.Stats.CritDamage;
        mapData.MonsterInfo.ManaRegen = monster.Stats.ManaRegen;
        mapData.ItemDropList = monster.DropList.GetItemDropList(monster.Stats.Level);
        mapData.EquipDropList = monster.DropList.GetEquipDropList(monster.Stats.Level);
    }

    public void LoadMap1MonsterSpawner(IMMonster other)
    {
        if (map.MapSwap.CurrentMap != MapEnum.Map0) return;

        mapData.Map1MonsterSpawnerInfo.Timer = map1MonsterSpawner.Timer;
        mapData.Map1MonsterSpawnerInfo.Counter = map1MonsterSpawner.Counter;
        mapData.Map1MonsterSpawnerInfo.CanSpawn = map1MonsterSpawner.CanSpawn;
        mapData.Map1MonsterSpawnerInfo.LockSpawn = map1MonsterSpawner.LockSpawn;
        mapData.Map1MonsterSpawnerInfo.MonsterInfos = new();

        List<Transform> monsters = map1MonsterSpawner.GetActiveMonsters();

        foreach (Transform monster in monsters)
        {
            if (monster == other) continue;

            IMMonster mons = monster.GetComponent<IMMonster>();
            MonsterInfo monsterInfo = new MonsterInfo();
            Vector3 posOffset = monster.position - map.Maps[0].position;
            monsterInfo.PosOffset = posOffset;
            monsterInfo.ZeroLevel = mons.Stats.ZeroLevel;
            monsterInfo.AttackType = mons.Stats.AttackType;

            mapData.Map1MonsterSpawnerInfo.MonsterInfos.Add(monsterInfo);
        }
    }

    public void LoadMap2MonsterSpawner(IMMonster other)
    {
        mapData.Map2MonsterSpawnerInfo.Timer = map2MonsterSpawner.Timer;
        mapData.Map2MonsterSpawnerInfo.Counter = map2MonsterSpawner.Counter;
        mapData.Map2MonsterSpawnerInfo.CanSpawn = map2MonsterSpawner.CanSpawn;
        mapData.Map2MonsterSpawnerInfo.LockSpawn = map2MonsterSpawner.LockSpawn;
        mapData.Map2MonsterSpawnerInfo.MonsterInfos = new();

        List<Transform> monsters = map2MonsterSpawner.GetActiveMonsters();

        foreach (Transform monster in monsters)
        {
            if (monster == other) continue;

            IMMonster mons = monster.GetComponent<IMMonster>();
            MonsterInfo monsterInfo = new MonsterInfo();
            Vector3 posOffset = monster.position - map.Maps[1].position;
            monsterInfo.PosOffset = posOffset;
            monsterInfo.ZeroLevel = mons.Stats.ZeroLevel;
            monsterInfo.AttackType = mons.Stats.AttackType;

            mapData.Map2MonsterSpawnerInfo.MonsterInfos.Add(monsterInfo);
        }
    }
}