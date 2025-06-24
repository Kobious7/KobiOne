using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSkillSO", menuName = "ScriptableObjects/TileSkill")]
public class TileSkillSO : ActiveSkillSO
{
    public bool SelfTarget;
    public bool OpponentTarget;
    public DamageStat Damage;
    public List<ActiveBuff> Buffs;
    public List<ActiveDebuff> Debuffs;
    public int ObjectMinSpawnCount;
    public int ObjectMaxSpawnCount;
    public int MinObjectToOp;
    public int MaxObjectToOp;
    public int AreaSquareRange;
    public string AreaString;
    public List<O> Area;
    public List<Vector3> ObjectSpawnPos;
    public Sprite ObjectSprite;
    public float ObjectSpeed;
    public Transform DObjectPrefab;
    public Transform DObjectHitFX;
}