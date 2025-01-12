using System.Collections.Generic;
using UnityEngine;

public class Skills : GMono
{
    private static Skills instance;

    public static Skills Instance => instance;

    [SerializeField, SkillButton(SkillButton.Q)] private SkillSO qSkill;

    public SkillSO QSkill => qSkill;

    [SerializeField, SkillButton(SkillButton.E)] private SkillSO eSkill;

    public SkillSO ESkill => eSkill;

    [SerializeField, SkillButton(SkillButton.SPACE)] private SkillSO spaceSkill;

    public SkillSO SpaceSkill => spaceSkill;

    [SerializeField] private Q q;

    public Q Q => q;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 Skills is allowed to exist");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadQ();
    }

    private void LoadQ()
    {
        if(q != null) return;

        q = GetComponentInChildren<Q>();
    }
}