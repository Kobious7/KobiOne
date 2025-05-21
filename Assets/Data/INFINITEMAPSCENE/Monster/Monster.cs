using UnityEngine;

namespace InfiniteMap
{
    public class Monster : GMono
    {
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private Transform rigModel;

        public Transform RigModel => rigModel;

        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        [SerializeField] private MonsterAnim anim;

        public MonsterAnim Anim => anim;

        [SerializeField] private MonsterStats stats;

        public MonsterStats Stats => stats;

        [SerializeField] private MonsterDropItemList dropItemList;

        public MonsterDropItemList DropItemList => dropItemList;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
            LoadRigModel();
            LoadAnimator();
            LoadAnimation();
            LoadStats();
            LoadDropItemList();
        }

        private void LoadModel()
        {
            if (model != null) return;

            model = transform.Find("Model");
        }

        private void LoadRigModel()
        {
            if (rigModel != null) return;

            rigModel = transform.Find("Model").GetChild(0);
        }

        private void LoadAnimator()
        {
            if (animator != null) return;

            animator = rigModel.GetComponent<Animator>();
        }

        private void LoadAnimation()
        {
            if (anim != animator) return;

            anim = GetComponentInChildren<MonsterAnim>();
        }

        private void LoadStats()
        {
            if (stats != null) return;

            stats = GetComponentInChildren<MonsterStats>();
        }

        private void LoadDropItemList()
        {
            if(dropItemList != null) return;

            dropItemList = GetComponentInChildren<MonsterDropItemList>();
        }
    }
}