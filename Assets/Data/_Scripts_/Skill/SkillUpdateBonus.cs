using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpdateBonus : SkillAb
{
    public event Action<List<OtherSourcesBonus>> OnSkillBonusChanged;
    public event Action<SkillTree> OnSkillTreeActiveChanged;

    [SerializeField] private List<OtherSourcesBonus> allTreeBonus;

    public List<OtherSourcesBonus> AllTreeBonus => allTreeBonus;

    [SerializeField] private List<OtherSourcesBonus> swordTreeBonus;
    [SerializeField] private WeaponType weaponType;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        NewAllBonusList();
        NewSwordBranchBonusList();
    }

    protected override void Start()
    {
        base.Start();
        InfiniteMapManager.Instance.Inventory.EquipWearing.OnEquipWearing += UpdateSkillTreesBonusWhenWeaponEquipped;
        InfiniteMapManager.Instance.Equipment.Unequip.OnEquipDisarming += UpdateSkillTreeBonusWhenWeaponUnequipped;
    }

    public List<OtherSourcesBonus> InitActiveSkillTreesBonus()
    {
        if (InfiniteMapManager.Instance.Equipment.Weapon.Level > 0)
        {
            WeaponSO weaponSO = InfiniteMapManager.Instance.Equipment.Weapon.EquipSO as WeaponSO;
            weaponType = weaponSO.WeaponType;
        }
        else
        {
            weaponType = WeaponType.Sword;
        }

        UpdateAllSkillTree();

        return allTreeBonus;
    }

    private void UpdateSkillTreesBonusWhenWeaponEquipped(InventoryEquip weapon)
    {
        if (weapon.EquipSO is not WeaponSO) return;

        WeaponSO weaponSO = weapon.EquipSO as WeaponSO;

        if (weaponSO.WeaponType == weaponType) return;

        weaponType = weaponSO.WeaponType;

        NewAllBonusList();
        UpdateAllSkillTree();

        OnSkillBonusChanged?.Invoke(allTreeBonus);
    }

    private void UpdateSkillTreeBonusWhenWeaponUnequipped(InventoryEquip weapon)
    {
        if (weapon.EquipSO is not WeaponSO) return;

        weaponType = WeaponType.Sword;

        NewAllBonusList();
        UpdateAllSkillTree();

        OnSkillBonusChanged?.Invoke(allTreeBonus);
    }

    public void UpdateAllSkillTree()
    {
        UpdateSwordTreeBonus(Skill.SkillTreeList[0]);
    }

    private void UpdateSwordTreeBonus(SkillTree swordTree)
    {
        if (swordTree.TypeRequires.Contains(weaponType))
        {
            UpdateBonusPerPassiveSkill(swordTreeBonus, swordTree.SkillList[5]);
            swordTree.IsActive = true;
        }
        else
        {
            NewSwordBranchBonusList();
            swordTree.IsActive = false;
        }

        OnSkillTreeActiveChanged?.Invoke(swordTree);
        CalculateBonusPerTree(swordTreeBonus);
    }

    public void UpdateBonusPerPassiveSkill(List<OtherSourcesBonus> list, SkillNode skill)
    {
        if (skill.SkillSO is not PassiveSkillSO || !skill.IsUnlocked) return;

        PassiveSkillSO passiveSkill = (PassiveSkillSO)skill.SkillSO;
        int level = skill.Level;

        foreach (var buff in passiveSkill.Buffs)
        {
            OtherSourcesBonus bonus = GetPassiveSkillBonusByStat(list, buff.Stat);
            bonus.FlatValue = buff.FlatValue + buff.FlatBonus * (level - 1);
            bonus.PercentValue = buff.PercentValue + buff.PercentBonus * (level - 1);
        }
    }

    private void CalculateBonusPerTree(List<OtherSourcesBonus> treeBonus)
    {
        foreach (var bonus in treeBonus)
        {
            int index = GetAllTreeBonusIndexByStat(bonus.Stat);
            allTreeBonus[index].FlatValue += bonus.FlatValue;
            allTreeBonus[index].PercentValue += bonus.PercentValue;
        }
    }

    public void UpdateBonusAfterUpgrading(SkillNode skill, int treeIndex)
    {
        if (treeIndex == 0 && Skill.SkillTreeList[treeIndex].TypeRequires.Contains(weaponType))
        {
            UpdateBonusBySkillTree(skill, swordTreeBonus);
        }

        OnSkillBonusChanged?.Invoke(allTreeBonus);
    }

    private void UpdateBonusBySkillTree(SkillNode skill, List<OtherSourcesBonus> list)
    {
        PassiveSkillSO passiveSkill = (PassiveSkillSO)skill.SkillSO;
        int level = skill.Level;

        foreach (var buff in passiveSkill.Buffs)
        {
            OtherSourcesBonus bonus = GetPassiveSkillBonusByStat(list, buff.Stat);
            bonus.FlatValue = buff.FlatValue + buff.FlatBonus * (level - 1);
            bonus.PercentValue = buff.PercentValue + buff.PercentBonus * (level - 1);
            OtherSourcesBonus previousBonus = new OtherSourcesBonus(bonus.Stat);
            previousBonus.FlatValue = buff.FlatValue + buff.FlatBonus * (level - 2);
            previousBonus.PercentValue = buff.PercentValue + buff.PercentBonus * (level - 2);
            OtherSourcesBonus allBonus = GetPassiveSkillBonusByStat(allTreeBonus, buff.Stat);
            allBonus.FlatValue = allBonus.FlatValue - previousBonus.FlatValue + bonus.FlatValue;
            allBonus.PercentValue = allBonus.PercentValue - previousBonus.PercentValue + bonus.PercentValue;
        }
    }

    public void NewAllBonusList()
    {
        allTreeBonus = new List<OtherSourcesBonus>
        {
            new OtherSourcesBonus(EquipStatType.Power), new OtherSourcesBonus(EquipStatType.Magic), new OtherSourcesBonus(EquipStatType.Strength),
            new OtherSourcesBonus(EquipStatType.DefenseP), new OtherSourcesBonus(EquipStatType.Dexterity), new OtherSourcesBonus(EquipStatType.Attack),
            new OtherSourcesBonus(EquipStatType.MagicAttack), new OtherSourcesBonus(EquipStatType.HP), new OtherSourcesBonus(EquipStatType.Defense),
            new OtherSourcesBonus(EquipStatType.Accuracy), new OtherSourcesBonus(EquipStatType.DamageRange), new OtherSourcesBonus(EquipStatType.Speed),
            new OtherSourcesBonus(EquipStatType.CritRate), new OtherSourcesBonus(EquipStatType.CritDamage), new OtherSourcesBonus(EquipStatType.ManaRegen)
        };
    }

    private void NewSwordBranchBonusList()
    {
        swordTreeBonus = new List<OtherSourcesBonus>
        {
            new OtherSourcesBonus(EquipStatType.Attack)
        };
    }

    private OtherSourcesBonus GetPassiveSkillBonusByStat(List<OtherSourcesBonus> list, EquipStatType stat)
    {
        foreach (var item in list)
            if (item.Stat == stat) return item;

        return null;
    }

    private int GetAllTreeBonusIndexByStat(EquipStatType stat)
    {
        for (int i = 0; i < allTreeBonus.Count; i++)
        {
            if (allTreeBonus[i].Stat == stat) return i;
        }

        return 0;
    }
}