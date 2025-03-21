using System;
using System.Collections.Generic;
using System.Linq;
using InfiniteMap;
using UnityEngine;

public class Skill : GMono
{
    [SerializeField] private int skillPoints;

    public int SkillPoints => skillPoints;

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
        characterData = Game.Instance.CharacterData;
        GetSkillData();
    }

    private void LoadSkillTrees()
    {
        if(skillTrees.Count > 0) return;

        skillTrees = GetComponentsInChildren<SkillTree>().ToList();
    }

    private void LoadUpgrading()
    {
        if(upgrading != null) return;

        upgrading = GetComponentInChildren<SkillUpgrade>();
    }

    private void LoadBonusUpdating()
    {
        if(bonusUpdating != null) return;

        bonusUpdating = GetComponentInChildren<SkillUpdateBonus>();
    }

    private void GetSkillData()
    {
        skillPoints = characterData.SkillPoints;

        qSkill = GetSkillNodeData(characterData.QSkill);
        eSkill = GetSkillNodeData(characterData.ESkill);
        spaceSkill = GetSkillNodeData(characterData.SpaceSkill);
        
        for(int i = 0; i < skillTrees.Count; i++)
        {
            skillTrees[i].T1B0.Level = characterData.SkillTreeLevels[i].T1B0Level;
            skillTrees[i].T2B1.Level = characterData.SkillTreeLevels[i].T2B1Level;
            skillTrees[i].T2B2.Level = characterData.SkillTreeLevels[i].T2B2Level;
            skillTrees[i].T2B3.Level = characterData.SkillTreeLevels[i].T2B3Level;
            skillTrees[i].T3B1.Level = characterData.SkillTreeLevels[i].T3B1Level;
            skillTrees[i].T3B2.Level = characterData.SkillTreeLevels[i].T3B2Level;
            skillTrees[i].T3B3.Level = characterData.SkillTreeLevels[i].T3B3Level;
        }
    }

    private CurrentSkillNode GetSkillNodeData(CurrentSkillNode currentSkillNodeData)
    {
        CurrentSkillNode skillNode = new();

        if(currentSkillNodeData.Level <= 0) return null;
        {
            skillNode.Level = currentSkillNodeData.Level;

            if(currentSkillNodeData.skillSO == null || skillNode.skillSO == null)
            {
                currentSkillNodeData.skillSO = skillTrees[currentSkillNodeData.TreeIndex].GetSkillNodeByName(currentSkillNodeData.NodeName).skillSO;
                skillNode.skillSO = currentSkillNodeData.skillSO;
            }
            
            skillNode.TreeIndex = currentSkillNodeData.TreeIndex;
            skillNode.NodeName = currentSkillNodeData.NodeName;
        }

        return skillNode;
    }

    public bool CheckSkillPoints(int skillPoint)
    {
        if(skillPoints >= skillPoint) return true;
        else return false;
    }

    public void DecreaseSkillPoints(int point)
    {
        skillPoints -= point;
        
        OnSkillPointChanging?.Invoke();
    }

    public void SetCurrentSkill(SkillNode skill, SkillButton button, int treeIndex)
    {
        if(button == SkillButton.Q)
        {
            qSkill = new();
            qSkill.Level = skill.Level;
            qSkill.skillSO = skill.skillSO;
            qSkill.TreeIndex = treeIndex;
            qSkill.NodeName = skill.skillSO.name;

            QDuplicateHandling(skill);
        }
        else if(button == SkillButton.E)
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
        if(eSkill.skillSO == skill.skillSO) eSkill = new();
        if(spaceSkill.skillSO == skill.skillSO) spaceSkill = new();
    }

    private void EDuplicateHandling(SkillNode skill)
    {
        if(qSkill.skillSO == skill.skillSO) qSkill = new();
        if(spaceSkill.skillSO == skill.skillSO) spaceSkill = new();
    }

    private void SpaceDuplicateHandling(SkillNode skill)
    {
        if(qSkill.skillSO == skill.skillSO) qSkill = new();
        if(eSkill.skillSO == skill.skillSO) eSkill = new();
    }
}