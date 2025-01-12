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

        [SerializeField] private Stat stat;

        public Stat Stat
        {
            get => stat;
            set => stat = value;
        }

        [SerializeField] private int amount;

        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        [SerializeField] private int percent;

        public int Percent
        {
            get => percent;
            set => percent = value;
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

        public bool DurationStack => durationStack;
    }
}