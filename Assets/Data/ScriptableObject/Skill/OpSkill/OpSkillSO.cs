using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OpSkillSO", menuName = "ScriptableObjects/OpSkill")]
public class OpSkillSO : ActiveSkillSO
{
    private SkillTarget mainTarget = SkillTarget.OPPONENT;
    
    public SkillTarget MainTarget => mainTarget;

    public bool Self;
    public DamageStat Damage;
    public List<ActiveBuff> Buffs;
    public List<ActiveDebuff> Debuffs;
    public int ObjectSpawnCount;
    public List<Vector3> ObjectSpawnPos;
    public Sprite ObjectSprite;
    public float ObjectSpeed;
}