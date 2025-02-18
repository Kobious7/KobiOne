using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfSkillSO", menuName = "ScriptableObjects/SelfSkill")]
public class SelfSkillSO : SkillSO
{
    private SkillTarget mainTarget = SkillTarget.SELF;

    public SkillTarget MainTarget => mainTarget;

    public SkillTarget AnotherTargets;
    public List<Buff> Buffs;
    public List<Debuff> Debuffs;
    public List<Effect> Effects;
    public List<DeEffect> DeEffects;
}