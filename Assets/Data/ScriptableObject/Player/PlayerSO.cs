using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    public bool IsLoaded;
    public string PlayerName;
    public int Level;
    public int CurrentExp;

    [Header("Potential")]
    public int AllPotentialPoints;
    public int RemainPoints;
    public Stat Power;
    public Stat Magic;
    public Stat Strength;
    public Stat Defense;
    public Stat Dexterity;

    [Header("Skill")]
    public int AllSkillPoints;
    public int SkillPoints;
    public CurrentSkillNode QSkill;
    public CurrentSkillNode ESkill;
    public CurrentSkillNode SpaceSkill;
    public List<SkillLevelList> SkillTreeLevels;

    [Header("Equipment")]
    public InventoryEquip Weapon;
    public InventoryEquip Helmet;
    public InventoryEquip Armor;
    public InventoryEquip Armwear;
    public InventoryEquip Boots;
    public InventoryEquip Special;

    [Header("Inventory")]
    public int PrimarionSoul;
    public List<InventoryItem> ItemList;
    public List<InventoryEquip> WeaponList, HelmetList, ArmorList, ArmwearList, BootsList, SpecialList;

    [Header("Infinite Map")]
    public int Distance;
    public int MapLevel;

    public void SetPlayerSO(PlayerSO player)
    {
        PlayerName = player.PlayerName;
        Level = player.Level;
        CurrentExp = player.CurrentExp;
        AllPotentialPoints = player.AllPotentialPoints;
        RemainPoints = player.RemainPoints;
        Power = new(player.Power);
        Magic = new(player.Magic);
        Strength = new(player.Strength);
        Defense = new(player.Defense);
        Dexterity = new(player.Dexterity);
        AllSkillPoints = player.AllSkillPoints;
        SkillPoints = player.SkillPoints;
        QSkill = new(player.QSkill);
        ESkill = new(player.ESkill);
        SpaceSkill = new(player.SpaceSkill);
        SkillTreeLevels = new(player.SkillTreeLevels);
        Weapon = new(player.Weapon);
        Helmet = new(player.Helmet);
        Armor = new(player.Armor);
        Boots = new(player.Boots);
        Special = new(player.Special);
        PrimarionSoul = player.PrimarionSoul;
        ItemList = new(player.ItemList);
        WeaponList = new(player.WeaponList);
        HelmetList = new(player.HelmetList);
        ArmorList = new(player.ArmorList);
        ArmwearList = new(player.ArmwearList);
        BootsList = new(player.BootsList);
        SpecialList = new(player.SpecialList);
        Distance = player.Distance;
        MapLevel = player.MapLevel;
    }

    public void SetPlayerSO(PlayerData playerData)
    {
        PlayerName = playerData.PlayerName;
        Level = playerData.Level;
        CurrentExp = playerData.CurrentExp;
        AllPotentialPoints = playerData.AllPotentialPoints;
        RemainPoints = playerData.RemainPoints;
        Power = playerData.Power;
        Magic = playerData.Magic;
        Strength = playerData.Strength;
        Defense = playerData.Defense;
        Dexterity = playerData.Dexterity;
        AllSkillPoints = playerData.AllSkillPoints;
        SkillPoints = playerData.SkillPoints;
        QSkill = playerData.QSkill;
        ESkill = playerData.ESkill;
        SpaceSkill = playerData.SpaceSkill;
        SkillTreeLevels = playerData.SkillTreeLevels;
        Weapon = playerData.Weapon;
        Helmet = playerData.Helmet;
        Armor = playerData.Armor;
        Boots = playerData.Boots;
        Special = playerData.Special;
        PrimarionSoul = playerData.PrimarionSoul;
        ItemList = playerData.ItemList;
        WeaponList = playerData.WeaponList;
        HelmetList = playerData.HelmetList;
        ArmorList = playerData.ArmorList;
        ArmwearList = playerData.ArmwearList;
        BootsList = playerData.BootsList;
        SpecialList = playerData.SpecialList;
        Distance = playerData.Distance;
        MapLevel = playerData.MapLevel;
    }
}