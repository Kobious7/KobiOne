using UnityEngine;

public abstract class SkillAb : GMono
{
    [SerializeField] private Skill skill;

    public Skill Skill => skill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkill();
    }

    private void LoadSkill()
    {
        if(skill != null) return;

        skill = transform.parent.GetComponent<Skill>();
    }
}