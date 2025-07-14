using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BSkillActivator : BSkillAb
{
    private Dictionary<SkillNode, SkillActivator> activators;

    public Dictionary<SkillNode, SkillActivator> Activators => activators;

    [SerializeField] private SkillButton currentSkill;

    public SkillButton CurrentSkill => currentSkill;

    [SerializeField] private bool isCasting;

    public bool IsCasting
    {
        get => isCasting;
        set => isCasting = value;
    }

    protected override void Start()
    {
        base.Start();

        activators = new();

        AddActivator(BSkill.QSkill);
        AddActivator(BSkill.ESkill);
        AddActivator(BSkill.SpaceSkill);
    }

    private void AddActivator(SkillNode skillNode)
    {
        if (skillNode.Level > 0 && skillNode.skillSO != null)
        {
            ActiveSkillSO activeSkillSO = skillNode.skillSO as ActiveSkillSO;
            Transform newActivator = Instantiate(activeSkillSO.Activator, transform.position, Quaternion.identity);

            newActivator.SetParent(transform);
            SkillActivator activator = newActivator.GetComponent<SkillActivator>();

            activators.Add(skillNode, activator);
        }
    }

    public void ActivateSkill(SkillNode skill, SkillButton button)
    {
        currentSkill = SkillButton.None;
        Battle.Instance.PlayerNextDamage = DamageType.None;

        if (activators.TryGetValue(skill, out var activator))
        {
            currentSkill = button;

            StartCoroutine(activator.Activate(skill, button, this));
        }
    }
}