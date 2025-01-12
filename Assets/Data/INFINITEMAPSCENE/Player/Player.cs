using UnityEngine;

namespace InfiniteMap
{
    public class Player : GMono
    {
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private GameObject rigModel;

        public GameObject RigModel => rigModel;

        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        [SerializeField] private Rigidbody rb;

        public Rigidbody Rb => rb;

        [SerializeField] private PlayerAnim anim;

        public PlayerAnim Anim => anim;

        [SerializeField] private Transform attackPoint;

        public Transform AttackPoint => attackPoint;

        [SerializeField] private Transform centerPoint;

        public Transform CenterPoint => centerPoint;

        [SerializeField] private PlayerShooting shooting;

        public PlayerShooting Shooting => shooting;

        [SerializeField] private PlayerStats stats;

        public PlayerStats Stats => stats;

        [SerializeField] private CapsuleCollider capsuleCollider;

        public CapsuleCollider CapsuleCollider => capsuleCollider;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
            LoadRigModel();
            LoadRigibody();
            LoadAnimator();
            LoadAnimation();
            LoadPoints();
            LoadShooting();
            LoadStats();
            LoadCapsuleCollider();
        }

        protected override void Start()
        {
            base.Start();

            if(Game.Instance.MapData.MapCanLoad)
            {
                transform.position = Game.Instance.MapData.PlayerInfo.PosOffset + Game.Instance.Map.Maps[0].position;
            }
        }

        private void LoadModel()
        {
            if (model != null) return;

            model = transform.Find("Model");
        }

        private void LoadRigModel()
        {
            if(rigModel != null) return;

            rigModel = transform.Find("Model").GetComponentInChildren<PlayerAdjustColliderInMap>().gameObject;
        }

        private void LoadRigibody()
        {
            if (rb != null) return;

            rb = GetComponent<Rigidbody>();
        }

        private void LoadAnimator()
        {
            if (animator != null) return;

            animator = transform.Find("Model").GetComponent<Animator>();
        }

        private void LoadAnimation()
        {
            if (anim != null) return;

            anim = GetComponentInChildren<PlayerAnim>();
        }

        private void LoadPoints()
        {
            if (attackPoint != null && centerPoint != null) return;

            attackPoint = transform.Find("AttackPoint");
            centerPoint = transform.Find("CenterPoint");
        }

        private void LoadShooting()
        {
            if (shooting != null) return;

            shooting = GetComponentInChildren<PlayerShooting>();
        }

        private void LoadStats()
        {
            if(stats != null) return;

            stats = GetComponentInChildren<PlayerStats>();
        }

        private void LoadCapsuleCollider()
        {
            if(capsuleCollider != null) return;

            capsuleCollider = GetComponent<CapsuleCollider>();
        }
    }
}