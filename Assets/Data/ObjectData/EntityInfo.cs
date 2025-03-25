using UnityEngine;

public abstract class EntityInfo
{
    public Vector3 PosOffset;
    public Sprite Model;
    public RuntimeAnimatorController Animator;
    public int Level;
    public int Attack;
    public int MagicAttack;
    public int HP;
    public int Defense;
    public int Accuracy;
    public int SlashDamage;
    public int SwordrainDamage;
    public float DamageRange;
    public float CritRate;
    public float CritDamage;
    public float ManaRegen;
}