using UnityEngine;

public class BMonsterStats : BEntityStats
{
    protected override void Start()
    {
        MonsterInfo monsterInfo = BattleManager.Instance.MapData.MonsterInfo;

        if (BattleManager.Instance.MapData.MapCanLoad)
        {
            attack = monsterInfo.Attack;
            magicAttack = monsterInfo.MagicAttack;
            maxHP = monsterInfo.HP;
            currentHP = maxHP;
            swordrainDamage = monsterInfo.SwordrainDamage;
            slashDamage = monsterInfo.SlashDamage;
            defense = monsterInfo.Defense;
            accuracy = monsterInfo.Accuracy;
            damageRange = monsterInfo.DamageRange;
            critRate = monsterInfo.CritRate;
            critDamage = monsterInfo.CritDamage;
            manaRegen = monsterInfo.ManaRegen;
        }

        InitBuff(); 
    }
}