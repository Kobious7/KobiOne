using UnityEngine;

public class DebuffObjectHandling : DebuffObjectAb
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
        DespawnDebuff();
    }

    private void TurnChange()
    {
        if (DebuffObj.DurationType == DurationType.TURN)
        {
            DebuffObj.Duration = DebuffObj.Duration - 1 <= 0 ? 0 : DebuffObj.Duration - 1;
            if (DebuffObj.Duration <= 0) return;
            CallDebuffChangeEvent();
        }
    }

    private void CycleChange()
    {
        if (DebuffObj.DurationType == DurationType.CYCLE)
        {
            DebuffObj.Duration = DebuffObj.Duration - 1 <= 0 ? 0 : DebuffObj.Duration - 1;
            if (DebuffObj.Duration <= 0) return;
            CallDebuffChangeEvent();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        previousFlatValue = 0;
        newFlatValue = 0;
        currentPercent = 0;
        DebuffHandling();
    }

    public void DebuffHandling()
    {
        BuffCalculate();
    }

    private void BuffCalculate()
    {
        BEntityStats stats = DebuffObj.Stats;
        StatBuffInfo statBuff = stats.BuffPercents[DebuffObj.TrueStatBuff];

        if (statBuff.IsPercentValue)
        {
            statBuff.PercentBonus = statBuff.PercentBonus - currentPercent + DebuffObj.PercentBuff;
            currentPercent = DebuffObj.PercentBuff;

            CalulatePercentStat(stats, statBuff, DebuffObj.TrueStatBuff);
        }
        else
        {
            previousFlatValue = (int)(statBuff.OriginFlatValue * currentPercent / 100);
            statBuff.PercentBonus -= currentPercent + DebuffObj.PercentBuff;
            currentPercent = DebuffObj.PercentBuff;
            newFlatValue = (int)(statBuff.OriginFlatValue * DebuffObj.PercentBuff / 100);

            CalulateFlatStat(stats, statBuff, DebuffObj.TrueStatBuff);
        }
    }

    private void CalulatePercentStat(BEntityStats stats, StatBuffInfo statBuff, EquipStatType equipStatType)
    {
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

    private void DespawnDebuff()
    {
        if (DebuffObj.Duration > 0) return;

        DebuffObj.PercentBuff = 0;

        BuffCalculate();

        CallDebuffDespawnEvent();
    }
    
    private void CallDebuffChangeEvent()
    {
        if (DebuffObj.Stats is BPlayerStats)
        {
            OpponentDebuffSpawner.Instance.CallOnBuffChangeEvent(DebuffObj);
        }

        if (DebuffObj.Stats is BMonsterStats)
        {
            PlayerDebuffSpawner.Instance.CallOnBuffChangeEvent(DebuffObj);
        }
    }

    private void CallDebuffDespawnEvent()
    {
        if (DebuffObj.Stats is BPlayerStats)
        {
            Debug.Log("Player Debuff");
            PlayerDebuffSpawner.Instance.Despawn(transform.parent);
            PlayerDebuffSpawner.Instance.CallOnBuffDespawnEvent(DebuffObj);
        }
       
        if (DebuffObj.Stats is BMonsterStats)
        {
            Debug.Log("Monster Debuff");
            OpponentDebuffSpawner.Instance.Despawn(transform.parent);
            OpponentDebuffSpawner.Instance.CallOnBuffDespawnEvent(DebuffObj);
        }
    }
}