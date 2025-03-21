using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSkillsUI : GMono
{
    [SerializeField] private Image qSkill;
    [SerializeField] private Image eSkill;
    [SerializeField] private Image spaceSkill;
    private Skill skill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCurrentSkills();
    }

    protected override void Start()
    {
        base.Start();
        SkillDetailUI.Instance.ActionBtns.OnSkillUsing += ShowCurrentSkillsUI;
        StartCoroutine(WaitNextFrame());
    }

    private void LoadCurrentSkills()
    {
        if(qSkill != null && eSkill != null && spaceSkill != null) return;

        qSkill = transform.Find("Q").Find("Model").GetComponentInChildren<Image>();
        eSkill = transform.Find("E").Find("Model").GetComponentInChildren<Image>();
        spaceSkill = transform.Find("Space").Find("Model").GetComponentInChildren<Image>();
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        skill = SkillSGT.Instance.Skill;
        ShowCurrentSkillsUI();
    }

    private void ShowCurrentSkillsUI()
    {
        qSkill.sprite = skill.QSkill != null ? skill.QSkill.skillSO.SkillIcon : null;
        eSkill.sprite = skill.ESkill != null ? skill.ESkill.skillSO.SkillIcon : null;
        spaceSkill.sprite = skill.SpaceSkill != null ? skill.SpaceSkill.skillSO.SkillIcon : null;
    }
}