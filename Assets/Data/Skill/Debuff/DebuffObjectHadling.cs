using UnityEngine;

namespace Battle
{
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
            if(DebuffObj.DurationType == DurationType.TURN)
            {
                DebuffObj.Duration = DebuffObj.Duration - 1 <= 0 ? 0 : DebuffObj.Duration - 1; 
            }
        }

        private void CycleChange()
        {
            if(DebuffObj.DurationType == DurationType.CYCLE)
            {
                DebuffObj.Duration = DebuffObj.Duration - 1 <= 0 ? 0 : DebuffObj.Duration - 1;
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
            EntityStats stats = DebuffObj.Stats;
            StatBuffInfo statBuff = stats.BuffPercents[DebuffObj.TrueStatBuff];

            if(statBuff.IsPercentValue)
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

        private void CalulatePercentStat(EntityStats stats, StatBuffInfo statBuff, EquipStatType equipStatType)
        {
            float percentAmount = statBuff.OriginPercentValue + statBuff.PercentBonus;
            switch(equipStatType)
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

        private void CalulateFlatStat(EntityStats stats, StatBuffInfo statBuff, EquipStatType equipStatType)
        {
            switch(equipStatType)
            {
                case EquipStatType.Defense:
                    stats.Defense = stats.Defense - previousFlatValue + newFlatValue;
                    break;
            }
        }


        private void DespawnDebuff()
        {
            if(DebuffObj.Duration > 0) return;

            DebuffObj.PercentBuff = 0;

            BuffCalculate();

            DebuffSpawner.Instance.Despawn(transform.parent);
        }

        private int GetStatByType(EquipStatType statType)
        {
            EntityStats stats = DebuffObj.Stats;

            switch(statType)
            {
                case EquipStatType.Defense:
                    return stats.Defense;
                case EquipStatType.SlashDamage:
                    return stats.SlashDamage;
                case EquipStatType.SwordrainDamage:
                    return stats.SwordrainDamage;
                default:
                    return 0;
            }
        }
    }
}