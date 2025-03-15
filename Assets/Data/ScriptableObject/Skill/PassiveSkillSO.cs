using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillSO", menuName = "ScriptableObjects/PassiveSkill")]
public class PassiveSkillSO : SkillSO
{
    public List<PassiveBuff> Buffs;
}