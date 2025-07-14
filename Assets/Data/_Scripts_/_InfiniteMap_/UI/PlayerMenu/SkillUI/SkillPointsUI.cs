using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class skillPointsUI : GMono
{
    [SerializeField] private TextMeshProUGUI skillPointsText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private void LoadText()
    {
        if(skillPointsText != null) return;

        skillPointsText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        ShowSkillPointsUI();
        
        SkillSGT.Instance.Skill.OnSkillPointChanging += ShowSkillPointsUI;
    }

    private void ShowSkillPointsUI()
    {
        skillPointsText.text = $"{SkillSGT.Instance.Skill.SkillPoints}";
    }
}