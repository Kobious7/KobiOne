using UnityEngine;

namespace Battle
{
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
            DespawnBuff();
        }

        private void TurnChange()
        {
            if(BuffObj.DurationType == DurationType.TURN)
            {
                BuffObj.Duration = BuffObj.Duration - 1 <= 0 ? 0 : BuffObj.Duration - 1; 
            }
        }

        private void CycleChange()
        {
            if(BuffObj.DurationType == DurationType.CYCLE)
            {
                BuffObj.Duration = BuffObj.Duration - 1 <= 0 ? 0 : BuffObj.Duration - 1;
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
            EntityStats stats = BuffObj.Stats;
            StatBuffInfo statBuff = stats.BuffPercents[BuffObj.TrueStatBuff];

            if(statBuff.IsPercentValue)
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

        private void CalulatePercentStat(EntityStats stats, StatBuffInfo statBuff, EquipStatType equipStatType)
        {
            Debug.Log(statBuff.OriginPercentValue);
            Debug.Log(statBuff.PercentBonus);
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


        private void DespawnBuff()
        {
            if(BuffObj.Duration > 0) return;

            BuffObj.PercentBuff = 0;

            BuffCalculate();

            DebuffSpawner.Instance.Despawn(transform.parent);
        }

        private int GetStatByType(EquipStatType statType)
        {
            EntityStats stats = BuffObj.Stats;

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