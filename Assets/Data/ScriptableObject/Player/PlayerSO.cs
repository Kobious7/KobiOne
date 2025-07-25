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

    public void SetPlayerSO(PlayerData playerData)
    {
        PlayerName = playerData.PlayerName;
        Level = playerData.Level;
        CurrentExp = playerData.CurrentExp;
        RemainPoints = playerData.RemainPoints;
        Power = playerData.Power;
        Magic = playerData.Magic;
        Strength = playerData.Strength;
        Defense = playerData.Defense;
        Dexterity = playerData.Dexterity;
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