using UnityEngine;

namespace Battle
{
    public class DeDebuffObjectHandling : DebuffObjectAb
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
            DespawnDebuff();
        }

        private void TurnChange()
        {
            if(DebuffObject.DurationType == DurationType.TURN)
            {
                DebuffObject.Duration = DebuffObject.Duration - 1 <= 0 ? 0 : DebuffObject.Duration - 1; 
            }
        }

        private void CycleChange()
        {
            if(DebuffObject.DurationType == DurationType.CYCLE)
            {
                DebuffObject.Duration = DebuffObject.Duration - 1 <= 0 ? 0 : DebuffObject.Duration - 1;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            DebuffHandling();
        }

        private int DebuffAmount(int stat)
        {
            fixedAmount = DebuffObject.Amount > 0 ? DebuffObject.Amount : 0;
            percentAmount = DebuffObject.Percent > 0 ? ((int)(stat * DebuffObject.Percent / 100)) : 0;
            return stat -= (fixedAmount + percentAmount);
        }

        private void DebuffHandling()
        {
            EntityStats stats = DebuffObject.Stats;

            switch(DebuffObject.Stat)
            {
                case Stat.HP:
                    stats.CurrentHP = DebuffAmount(stats.CurrentHP);
                    break;
                case Stat.VHP:
                    break;
                case Stat.SLASHDAMAGE:
                    stats.SlashDamage = DebuffAmount(stats.SlashDamage);
                    break;
                case Stat.SWORDDAMAGE:
                    stats.SwordrainDamage = DebuffAmount(stats.SwordrainDamage);
                    break;
            }
        }

        private void DespawnDebuff()
        {
            if(DebuffObject.Duration > 0) return;

            EntityStats stats = DebuffObject.Stats;

            switch(DebuffObject.Stat)
            {
                case Stat.HP:
                    stats.CurrentHP += (fixedAmount + percentAmount);
                    break;
                case Stat.VHP:
                    break;
                case Stat.SLASHDAMAGE:
                    stats.SlashDamage += (fixedAmount + percentAmount);
                    break;
                case Stat.SWORDDAMAGE:
                    stats.SwordrainDamage += (fixedAmount + percentAmount);
                    break;
            }

            DebuffSpawner.Instance.Despawn(transform.parent);
            return;
        }
    }
}