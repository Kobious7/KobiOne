using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSkillsUI : GMono
{
    [SerializeField] private Image qSkill, eSkill, spaceSkill, qBan, eBan, spaceBan;
    private Skill skill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        qSkill = transform.Find("Q").Find("Model").GetComponent<Image>();
        eSkill = transform.Find("E").Find("Model").GetComponent<Image>();
        spaceSkill = transform.Find("Space").Find("Model").GetComponent<Image>();
        qBan = transform.Find("Q").Find("Ban").GetComponent<Image>();
        eBan = transform.Find("E").Find("Ban").GetComponent<Image>();
        spaceBan = transform.Find("Space").Find("Ban").GetComponent<Image>();
    }

    protected override void Start()
    {
        base.Start();
        SkillDetailUI.Instance.ActionBtns.OnSkillUsing += ShowCurrentSkillsUI;
        StartCoroutine(WaitNextFrame());
    }


    private IEnumerator WaitNextFrame()
    {
        yield return null;
        skill = SkillSGT.Instance.Skill;
        skill.BonusUpdating.OnSkillTreeActiveChanged += CheckBan;
        ShowCurrentSkillsUI();
    }

    private void ShowCurrentSkillsUI()
    {
        qSkill.sprite = skill.QSkill != null && skill.QSkill.skillSO != null ? skill.QSkill.skillSO.SkillIcon : null;
        qBan.gameObject.SetActive(skill.QSkill == null ? false : skill.QSkill != null && skill.SkillTrees[skill.QSkill.TreeIndex].IsActive ? false : true);

        eSkill.sprite = skill.ESkill != null && skill.ESkill.skillSO != null ? skill.ESkill.skillSO.SkillIcon : null;
        eBan.gameObject.SetActive(skill.ESkill == null ? false : skill.ESkill != null && skill.SkillTrees[skill.ESkill.TreeIndex].IsActive ? false : true);

        spaceSkill.sprite = skill.SpaceSkill != null && skill.SpaceSkill.skillSO != null ? skill.SpaceSkill.skillSO.SkillIcon : null;
        spaceBan.gameObject.SetActive(skill.SpaceSkill == null ? false : skill.SpaceSkill != null && skill.SkillTrees[skill.SpaceSkill.TreeIndex].IsActive ? false : true);
    }

    private void CheckBan(SkillTree skillTree)
    {
        if (skill.QSkill != null && skill.SkillTrees[skill.QSkill.TreeIndex] == skillTree)
        {
            qBan.gameObject.SetActive(skill.SkillTrees[skill.QSkill.TreeIndex].IsActive ? false : true);
        }
        else
        {
            qBan.gameObject.SetActive(false);
        }

        if (skill.ESkill != null && skill.SkillTrees[skill.ESkill.TreeIndex] == skillTree)
        {
            eBan.gameObject.SetActive(skill.SkillTrees[skill.ESkill.TreeIndex].IsActive ? false : true);
        }
        else
        {
            eBan.gameObject.SetActive(false);
        }

        if (skill.SpaceSkill != null && skill.SkillTrees[skill.SpaceSkill.TreeIndex] == skillTree)
        {
            spaceBan.gameObject.SetActive(skill.SkillTrees[skill.SpaceSkill.TreeIndex].IsActive ? false : true);
        }
        else
        {
            spaceBan.gameObject.SetActive(false);
        }
    }
}