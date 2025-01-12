using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OpSkillSO", menuName = "ScriptableObjects/OpSkill")]
public class OpSkillSO : SkillSO
{
    private SkillTarget mainTarget = SkillTarget.OPPONENT;

    public SkillTarget MainTarget => mainTarget;

    [HideByOp] public SkillTarget AnotherTargets;

    public int Damage;
    public List<Buff> Buffs;
    public List<Debuff> Debuffs;
    public List<Effect> Effects;
    public List<DeEffect> DeEffects;
    public int ObjectSpawnCount;
    public List<Vector3> ObjectSpawnPos;
    public Sprite ObjectSprite;
    public float ObjectSpeed;
}