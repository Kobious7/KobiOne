using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSkillSO", menuName = "ScriptableObjects/TileSkill")]
public class TileSkillSO : ActiveSkillSO
{
    private SkillTarget mainTarget = SkillTarget.TILE;

    public SkillTarget MainTarget => mainTarget;

    public bool SelfTarget;
    public bool OpponentTarget;
    public DamageStat Damage;
    public List<ActiveBuff> Buffs;
    public List<ActiveDebuff> Debuffs;
    public int ObjectSpawnCount;
    public string AreaString;
    public List<O> Area;
    public List<Vector3> ObjectSpawnPos;
    public Sprite ObjectSprite;
    public float ObjectSpeed;
}