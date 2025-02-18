using System.Collections.Generic;
using UnityEngine;

public class Skills : GMono
{
    private static Skills instance;

    public static Skills Instance => instance;

    [SerializeField] private SkillSO qSkill;

    public SkillSO QSkill => qSkill;

    [SerializeField] private SkillSO eSkill;

    public SkillSO ESkill => eSkill;

    [SerializeField] private SkillSO spaceSkill;

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