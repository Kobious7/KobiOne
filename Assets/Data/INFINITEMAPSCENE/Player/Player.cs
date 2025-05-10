using UnityEngine;

namespace InfiniteMap
{
    public class Player : GMono
    {
        [SerializeField] private bool isUIOpening;

        public bool IsUIOpening
        {
            get => isUIOpening;
            set => isUIOpening = value;
        }
        
        [SerializeField] private Transform model;

        public Transform Model => model;

        [SerializeField] private Transform rigModel;

        public Transform RigModel => rigModel;

        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        [SerializeField] private Rigidbody rb;

        public Rigidbody Rb => rb;

        [SerializeField] private Rigidbody2D rb2D;

        public Rigidbody2D Rb2D => rb2D;

        [SerializeField] private PlayerAnim anim;

        public PlayerAnim Anim => anim;

        [SerializeField] private PlayerAttackPoint attackPoint;

        public PlayerAttackPoint AttackPoint => attackPoint;

        [SerializeField] private Transform centerPoint;

        public Transform CenterPoint => centerPoint;

        [SerializeField] private PlayerShooting shooting;

        public PlayerShooting Shooting => shooting;

        [SerializeField] private PlayerStats stats;

        public PlayerStats Stats => stats;

        [SerializeField] private CapsuleCollider2D capsuleCollider2D;

        public CapsuleCollider2D CapsuleCollider2D => capsuleCollider2D;

        [SerializeField] private PlayerSpriteSwap spriteSwap;

        public PlayerSpriteSwap SpriteSwap => spriteSwap;

        [SerializeField] private PlayerMeleeAttack meleeAttack;

        public PlayerMeleeAttack MeleeAttack => meleeAttack;

        [SerializeField] private PLayerRangeAttack rangedAttack;

        public PLayerRangeAttack RangedAttack => rangedAttack;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
            LoadRigModel();
            LoadRigibody();
            LoadRigibody2D();
            LoadAnimator();
            LoadAnimation();
            LoadAttackPoint();
            LoadCenterPoint();
            LoadShooting();
            LoadStats();
            LoadCapsuleCollider();
            //LoadSpriteSwap();
            LoadMeleeAttack();
            LoadRangedAttack();
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

            rigModel = transform.Find("Model").Find("RigModel");
        }

        private void LoadRigibody()
        {
            if (rb != null) return;

            rb = GetComponentInChildren<Rigidbody>();
        }

        private void LoadRigibody2D()
        {
            if(rb2D != null) return;

            rb2D = GetComponent<Rigidbody2D>();
        }

        private void LoadAnimator()
        {
            if (animator != null) return;

            animator = transform.Find("Model").Find("RigModel").GetComponentInChildren<Animator>();
        }

        private void LoadAnimation()
        {
            if (anim != null) return;

            anim = GetComponentInChildren<PlayerAnim>();
        }

        private void LoadAttackPoint()
        {
            if(attackPoint != null) return;

            attackPoint = GetComponentInChildren<PlayerAttackPoint>();
        }

        private void LoadCenterPoint()
        {
            if (centerPoint != null) return;

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
            if(capsuleCollider2D != null) return;

            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        private void LoadSpriteSwap()
        {
            if(spriteSwap != null) return;

            spriteSwap = rigModel.GetComponent<PlayerSpriteSwap>();
        }

        private void LoadMeleeAttack()
        {
            if(meleeAttack != null) return;

            meleeAttack = GetComponentInChildren<PlayerMeleeAttack>();
        }

        private void LoadRangedAttack()
        {
            if(rangedAttack != null) return;

            rangedAttack = GetComponentInChildren<PLayerRangeAttack>();
        }
    }
}