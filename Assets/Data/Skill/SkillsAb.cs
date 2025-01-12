using UnityEngine;

public abstract class SkillsAb : GMono
{
    [SerializeField] private Skills skills;

    public Skills Skills => skills;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkills();
    }

    private void LoadSkills()
    {
        if(skills != null) return;

        skills = transform.parent.GetComponent<Skills>();
    }
}