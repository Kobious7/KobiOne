using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill : GMono
{
    [SerializeField] private int skillPoints;
    [SerializeField] private SkillNode qSkill;

    public SkillNode QSkill => qSkill;

    [SerializeField] private SkillNode eSkill;

    public SkillNode ESkill => eSkill;

    [SerializeField] private SkillNode spaceSkill;

    public SkillNode SpaceSkill => spaceSkill;
    
    [SerializeField] private List<SkillTree> skillTrees;

    public List<SkillTree> SkillTrees => skillTrees;

    [SerializeField] private SkillUpgrade upgrading;

    public SkillUpgrade Upgrading => upgrading;

    [SerializeField] private SkillUpdateBonus bonusUpdating;

    public SkillUpdateBonus BonusUpdating => bonusUpdating;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTrees();
        LoadUpgrading();
        LoadBonusUpdating();
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

    public bool CheckSkillPoints(int skillPoint)
    {
        if(skillPoints >= skillPoint) return true;
        else return false;
    }

    public void DecreaseSkillPoints(int point)
    {
        skillPoints -= point;
    }

    public void SetCurrentSkill(SkillNode skill, SkillButton button)
    {
        if(button == SkillButton.Q)
        {
            qSkill = skill;

            QDuplicateHandling(skill);
        }
        else if(button == SkillButton.E)
        {
            eSkill = skill;

            EDuplicateHandling(skill);
        }
        else
        {
            spaceSkill = skill;
            
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