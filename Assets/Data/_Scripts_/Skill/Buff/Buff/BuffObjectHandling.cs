using System;
using UnityEngine;

public class BuffObjectHandling : BuffObjectAb
{
    [SerializeField] private int previousFlatValue;
    [SerializeField] private int newFlatValue;
    [SerializeField] private float currentPercent;

    protected override void Start()
    {
        base.Start();
        Battle.Instance.OnTurnChange += TurnChange;
        Battle.Instance.OnCycleChange += CycleChange;
    }

    private void FixedUpdate()
    {
        if (BuffObj.DurationType == DurationType.NextStrike)
        {
            DespawnNextStrikeBuff();
        }
        else
        {
            DespawnBuff();
        }
    }

    private void TurnChange()
    {
        if (BuffObj.DurationType == DurationType.TURN)
        {
            BuffObj.Duration = BuffObj.Duration - 1 <= 0 ? 0 : BuffObj.Duration - 1;

            if (BuffObj.Duration <= 0) return;
            CallBuffChangeEvent();
        }
    }

    private void CycleChange()
    {
        if (BuffObj.DurationType == DurationType.CYCLE)
        {
            BuffObj.Duration = BuffObj.Duration - 1 <= 0 ? 0 : BuffObj.Duration - 1;
            
            if (BuffObj.Duration <= 0) return;
            CallBuffChangeEvent();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        previousFlatValue = 0;
        newFlatValue = 0;
        currentPercent = 0;
        BuffHandling();
    }

    public void BuffHandling()
    {
        BuffCalculate();
    }

    private void BuffCalculate()
    {
        BEntityStats stats = BuffObj.Stats;
        StatBuffInfo statBuff = stats.BuffPercents[BuffObj.TrueStatBuff];

        if (statBuff.IsPercentValue)
        {
            statBuff.PercentBonus = statBuff.PercentBonus - currentPercent + BuffObj.PercentBuff;
            currentPercent = BuffObj.PercentBuff;

            CalulatePercentStat(stats, statBuff, BuffObj.TrueStatBuff);
        }
        else
        {
            previousFlatValue = (int)(statBuff.OriginFlatValue * currentPercent / 100);
            statBuff.PercentBonus -= currentPercent + BuffObj.PercentBuff;
            currentPercent = BuffObj.PercentBuff;
            newFlatValue = (int)(statBuff.OriginFlatValue * BuffObj.PercentBuff / 100);

            CalulateFlatStat(stats, statBuff, BuffObj.TrueStatBuff);
        }
    }

    private void CalulatePercentStat(BEntityStats stats, StatBuffInfo statBuff, EquipStatType equipStatType)
    {
        Debug.Log(statBuff.OriginPercentValue);
        Debug.Log(statBuff.PercentBonus);
        float percentAmount = statBuff.OriginPercentValue + statBuff.PercentBonus;
        switch (equipStatType)
        {
            case EquipStatType.DamageRange:
                stats.DamageRange = percentAmount;
                break;
            case EquipStatType.CritRate:
                stats.CritRate = percentAmount;
                break;
            case EquipStatType.CritDamage:
                stats.CritDamage = percentAmount;
                break;
        }
    }

    private void CalulateFlatStat(BEntityStats stats, StatBuffInfo statBuff, EquipStatType equipStatType)
    {
        switch (equipStatType)
        {
            case EquipStatType.Attack:
                stats.Attack = stats.Attack - previousFlatValue + newFlatValue;
                stats.SlashDamage = stats.SlashDamage - (int)(previousFlatValue / 3) + (int)(newFlatValue / 3);
                break;
            case EquipStatType.MagicAttack:
                stats.MagicAttack = stats.MagicAttack - previousFlatValue + newFlatValue;
                break;
            case EquipStatType.HP:
                stats.MaxHP = stats.MaxHP - previousFlatValue + newFlatValue;
                break;
            case EquipStatType.CurrentHPByMaxHP:
                stats.CurrentHP = stats.CurrentHP - previousFlatValue + newFlatValue;
                break;
            case EquipStatType.SlashDamage:
                stats.SlashDamage = stats.SlashDamage - previousFlatValue + newFlatValue;
                break;
            case EquipStatType.SwordrainDamage:
                stats.SwordrainDamage = stats.SwordrainDamage - previousFlatValue + newFlatValue;
                break;
            case EquipStatType.Defense:
                stats.Defense = stats.Defense - previousFlatValue + newFlatValue;
                break;
            case EquipStatType.Accuracy:
                stats.Accuracy = stats.Accuracy - previousFlatValue + newFlatValue;
                break;
        }
    }

    private void DespawnBuff()
    {
        if (BuffObj.Duration > 0) return;

        BuffObj.PercentBuff = 0;

        BuffCalculate();

        CallBuffDespawnEvent();
        BuffObj.Activator.EndSkillEffect();
    }

    private void DespawnNextStrikeBuff()
    {
        if (Battle.Instance.PlayerNextDamage != BuffObj.DamageType || !Battle.Instance.PlayerCrit) return;

        BuffObj.PercentBuff = 0;
        Battle.Instance.PlayerCrit = false;

        BuffCalculate();

        CallBuffDespawnEvent();
        BuffObj.Activator.EndSkillEffect();
    }

    private void CallBuffChangeEvent()
    {
        if (BuffObj.Stats is BPlayerStats)
        {
            PlayerBuffSpawner.Instance.CallOnBuffChangeEvent(BuffObj);
        }
        
        if(BuffObj.Stats is BMonsterStats)
        {
            OpponentBuffSpawner.Instance.CallOnBuffChangeEvent(BuffObj);
        }
    }

    private void CallBuffDespawnEvent()
    {
        if (BuffObj.Stats is BPlayerStats)
        {
            PlayerBuffSpawner.Instance.Despawn(transform.parent);
            PlayerBuffSpawner.Instance.CallOnBuffDespawnEvent(BuffObj);
        }

        if (BuffObj.Stats is BMonsterStats)
        {
            OpponentBuffSpawner.Instance.Despawn(transform.parent);
            OpponentBuffSpawner.Instance.CallOnBuffDespawnEvent(BuffObj);
        }
    }
}