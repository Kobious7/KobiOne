using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManagerUI : GMono
{
    [SerializeField] private List<SkillTreeUI> skillTreeUIs;

    private List<SkillTree> skillTrees;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTreeUIs();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private void LoadSkillTreeUIs()
    {
        if (skillTreeUIs.Count > 0) return;

        skillTreeUIs = transform.GetComponent<ScrollRect>().content.GetComponentsInChildren<SkillTreeUI>().ToList();
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        InfiniteMapManager.Instance.Skill.BonusUpdating.OnSkillTreeActiveChanged += CheckSkillTree;
        InfiniteMapManager.Instance.Skill.BonusUpdating.OnSkillTreeActiveChanged += ResetButtons;
        skillTrees = SkillSGT.Instance.Skill.SkillTrees;
        ShowSkillTrees();
    }

    private void ShowSkillTrees()
    {
        for (int i = 0; i < skillTreeUIs.Count; i++)
        {
            skillTreeUIs[i].ShowSkillTree(skillTrees[i], i);
        }
    }

    private void CheckSkillTree(SkillTree skillTree)
    {
        skillTreeUIs[skillTree.Index].CheckSkillTreeCompatible(skillTree);
    }

    private void ResetButtons(SkillTree skillTree)
    {
        for (int i = 0; i < skillTreeUIs.Count; i++)
        {
            skillTreeUIs[i].ResetUpgradeButton(skillTrees[i], i);
        }
    }
}