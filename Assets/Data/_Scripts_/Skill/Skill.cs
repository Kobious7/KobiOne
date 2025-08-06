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

    [SerializeField] private List<SkillTree> skillTreeList;

    public List<SkillTree> SkillTreeList => skillTreeList;

    [SerializeField] private SkillUpgrade upgrading;

    public SkillUpgrade Upgrading => upgrading;

    [SerializeField] private SkillUpdateBonus bonusUpdating;

    public SkillUpdateBonus BonusUpdating => bonusUpdating;

    [SerializeField] private SkillUnlock skillUnlock;

    public SkillUnlock SkillUnlock => skillUnlock;

    public event Action OnSkillPointChanging;
    private PlayerSO playerData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTreeList();
        LoadUpgrading();
        LoadBonusUpdating();
        if (skillUnlock == null) skillUnlock = GetComponentInChildren<SkillUnlock>();
    }

    private void LoadSkillTreeList()
    {
        if (skillTreeList.Count > 0) return;

        skillTreeList = GetComponentsInChildren<SkillTree>().ToList();
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

    public void GetSkillData()
    {
        playerData = InfiniteMapManager.Instance.PlayerData;
        skillPoints = playerData.SkillPoints;
        trueSkillPoints = playerData.AllSkillPoints;

        qSkill = GetSkillNodeData(playerData.QSkill);
        eSkill = GetSkillNodeData(playerData.ESkill);
        spaceSkill = GetSkillNodeData(playerData.SpaceSkill);

        for (int i = 0; i < skillTreeList.Count; i++)
        {
            for (int j = 0; j < skillTreeList[i].SkillList.Count; j++)
            {
                skillTreeList[i].SkillList[j].Level = playerData.SkillTreeLevels[i].SkillLevel[j];
                skillTreeList[i].SkillList[j].IsUnlocked = skillTreeList[i].SkillList[j].Level > 0 ? true : false;
            }
        }
    }

    private CurrentSkillNode GetSkillNodeData(CurrentSkillNode currentSkillNodeData)
    {
        CurrentSkillNode skillNode = new();

        if (currentSkillNodeData.Level <= 0) return null;

        skillNode.Level = currentSkillNodeData.Level;
        skillNode.IsUnlocked = true;
        skillNode.SkillSO = currentSkillNodeData.SkillSO;
        skillNode.TreeIndex = currentSkillNodeData.TreeIndex;

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

        //Save Data
        playerData.SkillPoints = skillPoints;
    }

    public void IncreaseSkillPoints(int point)
    {
        skillPoints += point;
        trueSkillPoints += point;

        OnSkillPointChanging?.Invoke();

        //Save Data
        playerData.SkillPoints = skillPoints;
        playerData.AllSkillPoints = trueSkillPoints;
    }

    public void SetCurrentSkill(SkillNode skill, SkillButton button, int treeIndex)
    {
        if (button == SkillButton.Q)
        {
            qSkill = new();
            qSkill.Level = skill.Level;
            qSkill.SkillSO = skill.SkillSO;
            qSkill.TreeIndex = treeIndex;

            QDuplicateHandling(skill);
        }
        else if (button == SkillButton.E)
        {
            eSkill = new();
            eSkill.Level = skill.Level;
            eSkill.SkillSO = skill.SkillSO;
            eSkill.TreeIndex = treeIndex;

            EDuplicateHandling(skill);
        }
        else
        {
            spaceSkill = new();
            spaceSkill.Level = skill.Level;
            spaceSkill.SkillSO = skill.SkillSO;
            spaceSkill.TreeIndex = treeIndex;

            SpaceDuplicateHandling(skill);
        }

        //Save Data
        playerData.QSkill = qSkill;
        playerData.ESkill = eSkill;
        playerData.SpaceSkill = spaceSkill;
    }

    private void QDuplicateHandling(SkillNode skill)
    {
        if (eSkill.SkillSO == skill.SkillSO) eSkill = new();
        if (spaceSkill.SkillSO == skill.SkillSO) spaceSkill = new();
    }

    private void EDuplicateHandling(SkillNode skill)
    {
        if (qSkill.SkillSO == skill.SkillSO) qSkill = new();
        if (spaceSkill.SkillSO == skill.SkillSO) spaceSkill = new();
    }

    private void SpaceDuplicateHandling(SkillNode skill)
    {
        if (qSkill.SkillSO == skill.SkillSO) qSkill = new();
        if (eSkill.SkillSO == skill.SkillSO) eSkill = new();
    }

    public void ResetAllSkill()
    {
        if (qSkill != null)
        {
            qSkill.Level = 1;
            playerData.QSkill.Level = qSkill.Level;
        }
        if (eSkill != null)
        {
            eSkill.Level = 1;
            playerData.ESkill.Level = eSkill.Level;
        }
        if (spaceSkill != null)
        {
            spaceSkill.Level = 1;
            playerData.SpaceSkill.Level = spaceSkill.Level;
        }

        foreach (var skillTree in skillTreeList)
        {
            foreach (var skill in skillTree.SkillList)
            {
                if (skill.IsUnlocked) skill.Level = 1;
                else skill.Level = 0;
            }

            //Save Data
            SaveSkillsLevel(skillTree);
        }

        skillPoints = trueSkillPoints;

        //Save Data
        playerData.SkillPoints = skillPoints;

        OnSkillPointChanging?.Invoke();

        bonusUpdating.NewAllBonusList();
        bonusUpdating.UpdateAllSkillTree();

        OnSkillPointsReset?.Invoke(bonusUpdating.AllTreeBonus);
    }

    private void SaveSkillsLevel(SkillTree skillTree)
    {
        for (int i = 0; i < skillTree.SkillList.Count; i++)
        {
            playerData.SkillTreeLevels[skillTree.Index].SkillLevel[i] = skillTree.SkillList[i].Level;
        }
    }
}