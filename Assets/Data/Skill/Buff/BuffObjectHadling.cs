using UnityEngine;

namespace Battle
{
    public class BuffObjectHandling : BuffObjectAb
    {
        [SerializeField] private int fixedAmount;
        [SerializeField] private int percentAmount;

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
            if(BuffObject.DurationType == DurationType.TURN)
            {
                BuffObject.Duration = BuffObject.Duration - 1 <= 0 ? 0 : BuffObject.Duration - 1; 
            }
        }

        private void CycleChange()
        {
            if(BuffObject.DurationType == DurationType.CYCLE)
            {
                BuffObject.Duration = BuffObject.Duration - 1 <= 0 ? 0 : BuffObject.Duration - 1;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            BuffHandling();
        }

        private int BuffAmount(int stat)
        {
            fixedAmount = BuffObject.Amount > 0 ? BuffObject.Amount : 0;
            percentAmount = BuffObject.Percent > 0 ? ((int)(stat * BuffObject.Percent / 100)) : 0;
            return stat += (fixedAmount + percentAmount);
        }

        private void BuffHandling()
        {
            EntityStats stats = BuffObject.Stats;

            switch(BuffObject.Stat)
            {
                case StatType.HP:
                    break;
                case StatType.VHP:
                    break;
                case StatType.SLASHDAMAGE:
                    stats.SlashDamage = BuffAmount(stats.SlashDamage);
                    break;
                case StatType.SWORDDAMAGE:
                    break;
            }
        }

        private void DespawnBuff()
        {
            if(BuffObject.Duration > 0) return;

            EntityStats stats = BuffObject.Stats;

            switch(BuffObject.Stat)
            {
                case StatType.HP:
                    break;
                case StatType.VHP:
                    break;
                case StatType.SLASHDAMAGE:
                    stats.SlashDamage -= (fixedAmount + percentAmount);
                    break;
                case StatType.SWORDDAMAGE:
                    break;
            }

            BuffSpawner.Instance.Despawn(transform.parent);
            return;
        }
    }
}