using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSkillSO", menuName = "ScriptableObjects/TileSkill")]
public class TileSkillSO : SkillSO
{
    private SkillTarget mainTarget = SkillTarget.TILE;

    public SkillTarget MainTarget => mainTarget;

    public SkillTarget AnotherTargets;
    public int Damage;
    public List<Buff> Buffs;
    public List<Debuff> Debuffs;
    public List<Effect> Effects;
    public List<DeEffect> DeEffects;
    public int ObjectSpawnCount;
    public List<O> Area;
    public List<Vector3> ObjectSpawnPos;
    public Sprite ObjectSprite;
    public float ObjectSpeed;
}