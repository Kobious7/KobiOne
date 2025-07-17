using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill : GMono
{
    public event Action<List<OtherSourcesBonus>> OnSkillPointsReset;
    [SerializeField] private int skillPoints;

    public int SkillPoints => skillPoints;

    [SerializeField] private int trueSkillPoints;

    public int TrueSkillPoints => trueSkillPoints;

    [SerializeField] private CurrentSkillNode qSkill;

    public CurrentSkillNode QSkill => qSkill;

    [SerializeField] private CurrentSkillNode eSkill;

    public CurrentSkillNode ESkill => eSkill;

    [SerializeField] private CurrentSkillNode spaceSkill;

    public CurrentSkillNode SpaceSkill => spaceSkill;

    [SerializeField] private List<SkillTree> skillTrees;

    public List<SkillTree> SkillTrees => skillTrees;

    [SerializeField] private SkillUpgrade upgrading;

    public SkillUpgrade Upgrading => upgrading;

    [SerializeField] private SkillUpdateBonus bonusUpdating;

    public SkillUpdateBonus BonusUpdating => bonusUpdating;

    public event Action OnSkillPointChanging;
    private CharacterSO characterData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTrees();
        LoadUpgrading();
        LoadBonusUpdating();
    }

    protected override void Start()
    {
        base.Start();
        characterData = InfiniteMapManager.Instance.CharacterData;
        GetSkillData();
    }

    private void LoadSkillTrees()
    {
        if (skillTrees.Count > 0) return;

        skillTrees = GetComponentsInChildren<SkillTree>().ToList();
    }

    private void LoadUpgrading()
    {
        if (upgrading != null) return;

        upgrading = GetComponentInChildren<SkillUpgrade>();
    }

    private void LoadBonusUpdating()
    {
        if (bonusUpdating != null) return;

        bonusUpdating = GetComponentInChildren<SkillUpdateBonus>();
    }

    private void GetSkillData()
    {
        skillPoints = characterData.SkillPoints;

        qSkill = GetSkillNodeData(characterData.QSkill);
        eSkill = GetSkillNodeData(characterData.ESkill);
        spaceSkill = GetSkillNodeData(characterData.SpaceSkill);

        for (int i = 0; i < skillTrees.Count; i++)
        {
            skillTrees[i].Root.Level = characterData.SkillTreeLevels[i].RootLevel;
            skillTrees[i].Attack1.Level = characterData.SkillTreeLevels[i].Attack1Level;
            skillTrees[i].Attack2.Level = characterData.SkillTreeLevels[i].Attack2Level;
            skillTrees[i].Attack3.Level = characterData.SkillTreeLevels[i].Attack3Level;
            skillTrees[i].Support1.Level = characterData.SkillTreeLevels[i].Support1Level;
            skillTrees[i].Support2.Level = characterData.SkillTreeLevels[i].Support2Level;
            skillTrees[i].Support3.Level = characterData.SkillTreeLevels[i].Support3Level;
        }
    }

    private CurrentSkillNode GetSkillNodeData(CurrentSkillNode currentSkillNodeData)
    {
        CurrentSkillNode skillNode = new();

        if (currentSkillNodeData.Level <= 0) return null;

        skillNode.Level = currentSkillNodeData.Level;

        if (currentSkillNodeData.skillSO == null || skillNode.skillSO == null)
        {
            currentSkillNodeData.skillSO = skillTrees[currentSkillNodeData.TreeIndex].GetSkillNodeByName(currentSkillNodeData.NodeName).skillSO;
            skillNode.skillSO = currentSkillNodeData.skillSO;
        }

        skillNode.TreeIndex = currentSkillNodeData.TreeIndex;
        skillNode.NodeName = currentSkillNodeData.NodeName;

        return skillNode;
    }

    public bool CheckSkillPoints(int skillPoint)
    {
        if (skillPoints >= skillPoint) return true;
        else return false;
    }

    public void DecreaseSkillPoints(int point)
    {
        skillPoints -= point;

        OnSkillPointChanging?.Invoke();
    }

    public void SetCurrentSkill(SkillNode skill, SkillButton button, int treeIndex)
    {
        if (button == SkillButton.Q)
        {
            qSkill = new();
            qSkill.Level = skill.Level;
            qSkill.skillSO = skill.skillSO;
            qSkill.TreeIndex = treeIndex;
            qSkill.NodeName = skill.skillSO.name;

            QDuplicateHandling(skill);
        }
        else if (button == SkillButton.E)
        {
            eSkill = new();
            eSkill.Level = skill.Level;
            eSkill.skillSO = skill.skillSO;
            eSkill.TreeIndex = treeIndex;
            eSkill.NodeName = skill.skillSO.name;

            EDuplicateHandling(skill);
        }
        else
        {
            spaceSkill = new();
            spaceSkill.Level = skill.Level;
            spaceSkill.skillSO = skill.skillSO;
            spaceSkill.TreeIndex = treeIndex;
            spaceSkill.NodeName = skill.skillSO.name;

            SpaceDuplicateHandling(skill);
        }
    }

    private void QDuplicateHandling(SkillNode skill)
    {
        if (eSkill.skillSO == skill.skillSO) eSkill = new();
        if (spaceSkill.skillSO == skill.skillSO) spaceSkill = new();
    }

    private void EDuplicateHandling(SkillNode skill)
    {
        if (qSkill.skillSO == skill.skillSO) qSkill = new();
        if (spaceSkill.skillSO == skill.skillSO) spaceSkill = new();
    }

    private void SpaceDuplicateHandling(SkillNode skill)
    {
        if (qSkill.skillSO == skill.skillSO) qSkill = new();
        if (eSkill.skillSO == skill.skillSO) eSkill = new();
    }

    public void ResetAllSkill()
    {
        if (qSkill != null) qSkill.Level = 1;
        if (eSkill != null) eSkill.Level = 1;
        if (spaceSkill != null) spaceSkill.Level = 1;

        foreach (var skillTree in skillTrees)
        {
            if (skillTree.Root.Level != 0) skillTree.Root.Level = 1;
            if (skillTree.Attack1.Level != 0) skillTree.Attack1.Level = 1;
            if (skillTree.Attack2.Level != 0) skillTree.Attack2.Level = 1;
            if (skillTree.Attack3.Level != 0) skillTree.Attack3.Level = 1;
            if (skillTree.Support1.Level != 0) skillTree.Support1.Level = 1;
            if (skillTree.Support2.Level != 0) skillTree.Support2.Level = 1;
            if (skillTree.Support3.Level != 0) skillTree.Support3.Level = 1;
        }

        skillPoints = trueSkillPoints;

        OnSkillPointChanging?.Invoke();

        bonusUpdating.NewAllBonusList();
        bonusUpdating.UpdateAllSkillTree();

        OnSkillPointsReset?.Invoke(bonusUpdating.AllTreeBonus);
    }
}