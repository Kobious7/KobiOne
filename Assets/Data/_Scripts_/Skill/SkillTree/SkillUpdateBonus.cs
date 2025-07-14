using System.Collections.Generic;
using UnityEngine;

public class SkillUpdateBonus : SkillAb
{
    [SerializeField] private List<PassiveSkillBonus> allTreeBonus;

    public List<PassiveSkillBonus> AllTreeBonus => allTreeBonus;

    [SerializeField] private List<PassiveSkillBonus> swordTreeBonus;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        NewAllBonusList();
        NewSwordBranchBonusList();
        UpdateCurrentBonus(Skill.SkillTrees[0]);
    }

    private void UpdateCurrentBonus(SkillTree skillTree)
    {
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Root);
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Attack1);
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Attack2);
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Attack3);
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Support1);
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Support2);
        UpdateBonusPerPassiveSkill(swordTreeBonus, skillTree.Support3);
    }

    private void UpdateBonusPerPassiveSkill(List<PassiveSkillBonus> list, SkillNode skill)
    {
        if(skill.skillSO is not PassiveSkillSO) return;

        PassiveSkillSO passiveSkill = (PassiveSkillSO)skill.skillSO;
        int level = skill.Level;

        foreach(var buff in passiveSkill.Buffs)
        {
            PassiveSkillBonus bonus = GetPassiveSkillBonusByStat(list, buff.Stat);
            bonus.FlatValue = buff.FlatValue + buff.FlatBonus * (level - 1);
            bonus.PercentValue = buff.PercentValue + buff.PercentValue * (level - 1);
            PassiveSkillBonus allBonus = GetPassiveSkillBonusByStat(allTreeBonus, buff.Stat);
            allBonus.FlatValue += bonus.FlatValue;
            allBonus.PercentValue += bonus.PercentValue;
        }

    }

    public void UpdateBonus(SkillNode skill, int treeIndex)
    {
        if(treeIndex == 0)
        {
            UpdateBonusBySkillTree(skill, swordTreeBonus);
        }

        InfiniteMapManager.Instance.Player.StatsSystem.UpdatePassiveSkillBonus(allTreeBonus);
    }

    private void UpdateBonusBySkillTree(SkillNode skill, List<PassiveSkillBonus> list)
    {
        PassiveSkillSO passiveSkill = (PassiveSkillSO)skill.skillSO;
        int level = skill.Level;

        foreach(var buff in passiveSkill.Buffs)
        {
            PassiveSkillBonus bonus = GetPassiveSkillBonusByStat(list, buff.Stat);
            bonus.FlatValue = buff.FlatValue + buff.FlatBonus * (level - 1);
            bonus.PercentValue = buff.PercentValue + buff.PercentBonus * (level - 1);
            PassiveSkillBonus previousBonus = new PassiveSkillBonus(bonus.Stat);
            previousBonus.FlatValue = buff.FlatValue + buff.FlatBonus * (level - 2);
            previousBonus.PercentValue = buff.PercentValue + buff.PercentBonus * (level - 2);
            PassiveSkillBonus allBonus = GetPassiveSkillBonusByStat(allTreeBonus, buff.Stat);
            allBonus.FlatValue = allBonus.FlatValue - previousBonus.FlatValue + bonus.FlatValue;
            allBonus.PercentValue = allBonus.PercentValue - previousBonus.PercentValue + bonus.PercentValue;
        }
    }

    private void NewAllBonusList()
    {
        allTreeBonus = new List<PassiveSkillBonus>
        {
            new PassiveSkillBonus(EquipStatType.Power), new PassiveSkillBonus(EquipStatType.Magic), new PassiveSkillBonus(EquipStatType.Strength),
            new PassiveSkillBonus(EquipStatType.DefenseP), new PassiveSkillBonus(EquipStatType.Dexterity), new PassiveSkillBonus(EquipStatType.Attack),
            new PassiveSkillBonus(EquipStatType.MagicAttack), new PassiveSkillBonus(EquipStatType.HP), new PassiveSkillBonus(EquipStatType.Defense),
            new PassiveSkillBonus(EquipStatType.Accuracy), new PassiveSkillBonus(EquipStatType.DamageRange), new PassiveSkillBonus(EquipStatType.Speed),
            new PassiveSkillBonus(EquipStatType.CritRate), new PassiveSkillBonus(EquipStatType.CritDamage), new PassiveSkillBonus(EquipStatType.ManaRegen)
        };
    }

    private void NewSwordBranchBonusList()
    {
        swordTreeBonus = new List<PassiveSkillBonus>
        {
            new PassiveSkillBonus(EquipStatType.Power), new PassiveSkillBonus(EquipStatType.Attack)
        };
    }

    private PassiveSkillBonus GetPassiveSkillBonusByStat(List<PassiveSkillBonus> list, EquipStatType stat)
    {
        foreach(var item in list)
            if(item.Stat == stat) return item;

        return null;
    }
}