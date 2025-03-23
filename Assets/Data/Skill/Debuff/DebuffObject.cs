using UnityEngine;

namespace Battle
{
    public class DebuffObject : GMono
    {
        [SerializeField] private EntityStats stats;

        public EntityStats Stats
        {
            get => stats;
            set => stats = value;
        }

        [SerializeField] private EquipStatType sourceStat;

        public EquipStatType SourceStat
        {
            get => sourceStat;
            set => sourceStat = value;
        }

        [SerializeField] private EquipStatType trueStatBuff;

        public EquipStatType TrueStatBuff
        {
            get => trueStatBuff;
            set => trueStatBuff = value;
        }

        [SerializeField] private int percentBuff;

        public int PercentBuff
        {
            get => percentBuff;
            set => percentBuff = value;
        }

        [SerializeField] private DurationType durationType;

        public DurationType DurationType
        {
            get => durationType;
            set => durationType = value;
        }

        [SerializeField] private int duration;

        public int Duration
        {
            get => duration;
            set => duration = value;
        }

        [SerializeField] private bool durationStack;

        public bool DurationStack
        {
            get => durationStack;
            set => durationStack = value;
        }

        [SerializeField] private bool percentStack;

        public bool PercentStack
        {
            get => percentStack;
            set => percentStack = value;
        }

        [SerializeField] private DebuffObjectHandling debuffHandler;

        public DebuffObjectHandling DebuffHandler => debuffHandler;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadDebuffHanlder();
        }

        private void LoadDebuffHanlder()
        {
            if(debuffHandler != null) return;

            debuffHandler = GetComponentInChildren<DebuffObjectHandling>();
        }
    }
}