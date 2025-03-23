using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfSkillSO", menuName = "ScriptableObjects/SelfSkill")]
public class SelfSkillSO : ActiveSkillSO
{
    public bool Opponent;
    public List<ActiveBuff> Buffs;
    public List<ActiveDebuff> Debuffs;
}