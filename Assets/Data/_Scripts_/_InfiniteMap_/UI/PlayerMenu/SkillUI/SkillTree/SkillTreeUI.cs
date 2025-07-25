using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillTreeUI : GMono
{
    [SerializeField] private List<SkillNodeUI> skillNodeUIList;
    [SerializeField] private Transform hint;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (skillNodeUIList.Count <= 0) skillNodeUIList = GetComponentsInChildren<SkillNodeUI>().ToList();

        hint = transform.Find("Hint");
    }

    public void ShowSkillTree(SkillTree skillTree, int treeIndex)
    {
        CheckSkillTreeCompatible(skillTree);

        for (int i = 0; i < skillTree.SkillList.Count; i++)
        {
            skillNodeUIList[i].ShowSkill(skillTree.SkillList[i], treeIndex, skillTree.IsActive);
        }
    }

    public void ResetUpgradeButton(SkillTree skillTree, int treeIndex)
    {
        for (int i = 0; i < skillTree.SkillList.Count; i++)
        {
            skillNodeUIList[i].UpdateClick(skillTree.SkillList[i], treeIndex, skillTree.IsActive);
        }
    }

    public void CheckSkillTreeCompatible(SkillTree skillTree)
    {
        if (skillTree.IsActive)
        {
            hint.gameObject.SetActive(false);
        }
        else
        {
            hint.gameObject.SetActive(true);
        }
    }
}