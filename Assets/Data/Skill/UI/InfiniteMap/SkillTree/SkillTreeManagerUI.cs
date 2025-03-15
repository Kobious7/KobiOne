using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        if(skillTreeUIs.Count > 0) return;

        skillTreeUIs = transform.GetComponent<ScrollRect>().content.GetComponentsInChildren<SkillTreeUI>().ToList();
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        skillTrees = SkillSGT.Instance.Skill.SkillTrees;
        ShowSkillTrees();
    }

    private void ShowSkillTrees()
    {
        for(int i = 0; i < skillTreeUIs.Count; i++)
        {
            skillTreeUIs[i].ShowSkillTree(skillTrees[i], i);
        }
    }
}