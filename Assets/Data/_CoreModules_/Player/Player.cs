using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : GMono
{
    private string activeScene;

    public string ActiveScene => activeScene;
    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private Transform rigModel;

    public Transform RigModel => rigModel;

    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    [SerializeField] private PlayerAnim anim;

    public PlayerAnim Anim => anim;

    [SerializeField] private PlayerAttackPoint attackPoint;

    public PlayerAttackPoint AttackPoint => attackPoint;

    [SerializeField] private Transform centerPoint;

    public Transform CenterPoint => centerPoint;

    [SerializeField] private PlayerShooting shooting;

    public PlayerShooting Shooting => shooting;

    [SerializeField] private PlayerMeleeAttack meleeAttack;

    public PlayerMeleeAttack MeleeAttack => meleeAttack;

    [SerializeField] private PlayerRangedAttack rangedAttack;

    public PlayerRangedAttack RangedAttack => rangedAttack;

    [SerializeField] private PlayerSwapWeapon swapWeapon;

    public PlayerSwapWeapon SwapWeapon => swapWeapon;

    [Header("Infinite Map")]
    [SerializeField] private bool isUIOpening;

    public bool IsUIOpening
    {
        get => isUIOpening;
        set => isUIOpening = value;
    }

    [SerializeField] private PlayerInfiniteMapStats infiniteMapStats;

    public PlayerInfiniteMapStats InfiniteMapStats => infiniteMapStats;

    [SerializeField] private Rigidbody infiniteMapRb;

    public Rigidbody InfiniteMapRB => infiniteMapRb;

    [SerializeField] private Rigidbody2D infiniteMapRb2D;

    public Rigidbody2D InfiniteMapRb2D => infiniteMapRb2D;

    [SerializeField] private CapsuleCollider2D infiniteMapCC2D;

    public CapsuleCollider2D InfiniteMapCC2D => infiniteMapCC2D;

    [SerializeField] private PlayerSpriteSwap spriteSwap;

    public PlayerSpriteSwap SpriteSwap => spriteSwap;

    [Header("Battle")]
    [SerializeField] private PlayerBattleMovement battleMovement;

    public PlayerBattleMovement BattleMovement => battleMovement;

    [SerializeField] private PlayerBattleStats battleStats;

    public PlayerBattleStats BattleStats => battleStats;

    [SerializeField] private CapsuleCollider battleCC;

    public CapsuleCollider BattleCC => battleCC;

    [SerializeField] private PlayerBattleSwordrain swordrain;

    public PlayerBattleSwordrain Swordrain => swordrain;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadRigModel();
        LoadAnimator();
        LoadAnimation();
        LoadAttackPoint();
        LoadCenterPoint();
        LoadShooting();
        LoadMeleeAttack();
        LoadRangedAttack();
        LoadSwapWeapon();

        activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "InfiniteMap")
        {
            LoadInfiniteMapStats();
            LoadInfiniteMapRigibody();
            LoadInfiniteMapRigibody2D();
            LoadInfiniteMapCapsuleCollider2D();
            //LoadSpriteSwap();
        }

        if (activeScene == "Battle")
        {
            LoadBattleMovement();
            LoadBattleStats();
            LoadBattleCapsuleCollider();
            LoadSwordrain();
        }
    }

    protected override void Start()
    {
        base.Start();

        if (activeScene == "InfiniteMap" && Game.Instance.MapData.MapCanLoad)
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
        if (rigModel != null) return;

        rigModel = transform.Find("Model").Find("RigModel");
    }

    private void LoadInfiniteMapRigibody()
    {
        if (infiniteMapRb != null) return;

        infiniteMapRb = GetComponentInChildren<Rigidbody>();
    }

    private void LoadInfiniteMapRigibody2D()
    {
        if (infiniteMapRb2D != null) return;

        infiniteMapRb2D = GetComponent<Rigidbody2D>();
    }

    private void LoadAnimator()
    {
        if (animator != null) return;

        animator = transform.Find("Model").Find("RigModel").GetComponent<Animator>();
    }

    private void LoadAnimation()
    {
        if (anim != null) return;

        anim = GetComponentInChildren<PlayerAnim>();
    }

    private void LoadAttackPoint()
    {
        if (attackPoint != null) return;

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

    private void LoadInfiniteMapStats()
    {
        if (infiniteMapStats != null) return;

        infiniteMapStats = GetComponentInChildren<PlayerInfiniteMapStats>();
    }

    private void LoadInfiniteMapCapsuleCollider2D()
    {
        if (infiniteMapCC2D != null) return;

        infiniteMapCC2D = GetComponent<CapsuleCollider2D>();
    }

    private void LoadSpriteSwap()
    {
        if (spriteSwap != null) return;

        spriteSwap = rigModel.GetComponent<PlayerSpriteSwap>();
    }

    private void LoadMeleeAttack()
    {
        if (meleeAttack != null) return;

        meleeAttack = GetComponentInChildren<PlayerMeleeAttack>();
    }

    private void LoadRangedAttack()
    {
        if (rangedAttack != null) return;

        rangedAttack = GetComponentInChildren<PlayerRangedAttack>();
    }

    private void LoadBattleMovement()
    {
        if (battleMovement != null) return;

        battleMovement = GetComponentInChildren<PlayerBattleMovement>();
    }

    private void LoadBattleStats()
    {
        if (battleStats != null) return;

        battleStats = GetComponentInChildren<PlayerBattleStats>();
    }

    private void LoadBattleCapsuleCollider()
    {
        if (battleCC != null) return;

        battleCC = GetComponent<CapsuleCollider>();
    }

    private void LoadSwordrain()
    {
        if (swordrain != null) return;

        swordrain = GetComponentInChildren<PlayerBattleSwordrain>();
    }

    private void LoadSwapWeapon()
    {
        if (swapWeapon != null) return;

        swapWeapon = GetComponentInChildren<PlayerSwapWeapon>();
    }
}