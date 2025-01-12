using UnityEngine;

namespace InfiniteMap
{
    public class Monster : GMono
    {
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private MonsterStats stats;

        public MonsterStats Stats => stats;

        [SerializeField] private MonsterDropItemList dropItemList;

        public MonsterDropItemList DropItemList => dropItemList;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
            LoadStats();
            LoadDropItemList();
        }

        private void LoadModel()
        {
            if (model != null) return;

            model = transform.Find("Model");
        }

        private void LoadStats()
        {
            if(stats != null) return;

            stats = GetComponentInChildren<MonsterStats>();
        }

        private void LoadDropItemList()
        {
            if(dropItemList != null) return;

            dropItemList = GetComponentInChildren<MonsterDropItemList>();
        }
    }
}