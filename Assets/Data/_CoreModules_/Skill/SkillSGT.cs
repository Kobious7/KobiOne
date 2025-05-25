using UnityEngine;

public class SkillSGT : GMono
{
    private static SkillSGT instance;

    public static SkillSGT Instance => instance;

    [SerializeField] private Skill skill;

    public Skill Skill => skill;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 SkillSGT is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillManager();
    }

    private void LoadSkillManager()
    {
        if(skill != null) return;

        skill = FindObjectOfType<Skill>();
    }
}