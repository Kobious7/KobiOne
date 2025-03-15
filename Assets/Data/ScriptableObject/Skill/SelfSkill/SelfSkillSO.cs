using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfSkillSO", menuName = "ScriptableObjects/SelfSkill")]
public class SelfSkillSO : ActiveSkillSO
{
    private SkillTarget mainTarget = SkillTarget.SELF;

    public SkillTarget MainTarget => mainTarget;

    public bool OpponentTarget;
    public List<ActiveBuff> Buffs;
    public List<Debuff> Debuffs;
}