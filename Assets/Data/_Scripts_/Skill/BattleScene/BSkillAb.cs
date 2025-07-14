using UnityEngine;

public abstract class BSkillAb : GMono
{
    [SerializeField] private BSkill bSkill;

    public BSkill BSkill => bSkill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillB();
    }

    private void LoadSkillB()
    {
        if(bSkill != null) return;

        bSkill = transform.parent.GetComponent<BSkill>();
    }
}