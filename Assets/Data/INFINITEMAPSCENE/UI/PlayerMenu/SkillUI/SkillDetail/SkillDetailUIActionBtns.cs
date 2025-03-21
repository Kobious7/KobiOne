using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailUIActionBtns : GMono
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button qBtn;
    [SerializeField] private Button eBtn;
    [SerializeField] private Button spaceBtn;
    public event Action<SkillNode, int> OnSkillUpgrading;
    public event Action OnSkillUsing;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAllActionBtns();
    }

    private void LoadAllActionBtns()
    {
        if(upgradeBtn != null && qBtn != null && eBtn != null && spaceBtn != null) return;

        upgradeBtn = transform.Find("UpgradeBtn").GetComponent<Button>();
        qBtn = transform.Find("QBtn").GetComponent<Button>();
        eBtn = transform.Find("EBtn").GetComponent<Button>();
        spaceBtn = transform.Find("SpaceBtn").GetComponent<Button>(); 
    }

    public void AddClickListeners(SkillNode skill, int treeIndex)
    {
        if(skill.Level > 0)
        {
            upgradeBtn.gameObject.SetActive(true);
            qBtn.gameObject.SetActive(true);
            eBtn.gameObject.SetActive(true);
            spaceBtn.gameObject.SetActive(true);

            if(skill.Level < skill.skillSO.MaxLevel)
            {
                upgradeBtn.onClick.RemoveAllListeners();
                upgradeBtn.onClick.AddListener(() => UpgradeClick(skill, treeIndex));
            }
            else
            {
                upgradeBtn.gameObject.SetActive(false);
            }

            if(skill.skillSO is ActiveSkillSO)
            {
                qBtn.onClick.RemoveAllListeners();
                qBtn.onClick.AddListener(() => QClick(skill, treeIndex));
                eBtn.onClick.RemoveAllListeners();
                eBtn.onClick.AddListener(() => EClick(skill, treeIndex));
                spaceBtn.onClick.RemoveAllListeners();
                spaceBtn.onClick.AddListener(() => SpaceClick(skill, treeIndex));
            }
            else
            {
                qBtn.gameObject.SetActive(false);
                eBtn.gameObject.SetActive(false);
                spaceBtn.gameObject.SetActive(false);
            }

        }
        else
        {
            upgradeBtn.gameObject.SetActive(false);
            qBtn.gameObject.SetActive(false);
            eBtn.gameObject.SetActive(false);
            spaceBtn.gameObject.SetActive(false);
        }
    }

    private void UpgradeClick(SkillNode skill, int treeIndex)
    {
        SkillSGT.Instance.Skill.Upgrading.Upgrade(skill, treeIndex);
        OnSkillUpgrading?.Invoke(skill, treeIndex);
    }

    private void QClick(SkillNode skill, int treeIndex)
    {
        SkillSGT.Instance.Skill.SetCurrentSkill(skill, SkillButton.Q, treeIndex);
        OnSkillUsing?.Invoke();
    }

    private void EClick(SkillNode skill, int treeIndex)
    {
        SkillSGT.Instance.Skill.SetCurrentSkill(skill, SkillButton.E, treeIndex);
        OnSkillUsing?.Invoke();
    }

    private void SpaceClick(SkillNode skill, int treeIndex)
    {
        SkillSGT.Instance.Skill.SetCurrentSkill(skill, SkillButton.Space, treeIndex);
        OnSkillUsing?.Invoke();
    }
}