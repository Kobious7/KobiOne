using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int Level;
    public int CurrentExp;
    public int AllPotentialPoints;
    public int RemainPoints;
    public Stat Power;
    public Stat Magic;
    public Stat Strength;
    public Stat Defense;
    public Stat Dexterity;
    public int AllSkillPoints;
    public int SkillPoints;
    public CurrentSkillNode QSkill;
    public CurrentSkillNode ESkill;
    public CurrentSkillNode SpaceSkill;
    public List<SkillLevelList> SkillTreeLevels;
    public InventoryEquip Weapon;
    public InventoryEquip Helmet;
    public InventoryEquip Armor;
    public InventoryEquip Armwear;
    public InventoryEquip Boots;
    public InventoryEquip Special;
    public int PrimarionSoul;
    public List<InventoryItem> ItemList;
    public List<InventoryEquip> WeaponList, HelmetList, ArmorList, ArmwearList, BootsList, SpecialList;
    public int Distance, MapLevel;

    public PlayerData(PlayerSO playerSO)
    {
        PlayerName = playerSO.PlayerName;
        Level = playerSO.Level;
        CurrentExp = playerSO.CurrentExp;
        AllPotentialPoints = playerSO.AllPotentialPoints;
        RemainPoints = playerSO.RemainPoints;
        Power = playerSO.Power;
        Magic = playerSO.Magic;
        Strength = playerSO.Strength;
        Defense = playerSO.Defense;
        Dexterity = playerSO.Dexterity;
        SkillPoints = playerSO.SkillPoints;
        QSkill = playerSO.QSkill;
        ESkill = playerSO.ESkill;
        SpaceSkill = playerSO.SpaceSkill;
        SkillTreeLevels = playerSO.SkillTreeLevels;
        Weapon = playerSO.Weapon;
        Helmet = playerSO.Helmet;
        Armor = playerSO.Armor;
        Boots = playerSO.Boots;
        Special = playerSO.Special;
        PrimarionSoul = playerSO.PrimarionSoul;
        ItemList = playerSO.ItemList;
        WeaponList = playerSO.WeaponList;
        HelmetList = playerSO.HelmetList;
        ArmorList = playerSO.ArmorList;
        ArmwearList = playerSO.ArmwearList;
        BootsList = playerSO.BootsList;
        SpecialList = playerSO.SpecialList;
        Distance = playerSO.Distance;
        MapLevel = playerSO.MapLevel;
    }
}