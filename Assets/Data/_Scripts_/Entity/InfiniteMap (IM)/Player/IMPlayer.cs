using UnityEngine;

public class IMPlayer : Entity
{
    [SerializeField] private bool isUIOpening;

    public bool IsUIOpening
    {
        get => isUIOpening;
        set => isUIOpening = value;
    }

    [SerializeField] private Rigidbody2D rb2D;

    public Rigidbody2D Rb2D => rb2D;

    [SerializeField] private IMPlayerMeleeAttack meleeAttack;

    public IMPlayerMeleeAttack MeleeAttack => meleeAttack;

    [SerializeField] private IMPlayerRangedAttack rangedAttack;

    public IMPlayerRangedAttack RangedAttack => rangedAttack;

    [SerializeField] private IMPlayerStats statsSystem;

    public IMPlayerStats StatsSystem => statsSystem;

    [SerializeField] private IMEntityShooting shooting;

    public IMEntityShooting Shooting => shooting;

    [SerializeField] private IMPlayerMovement movement;

    public IMPlayerMovement Movement => movement;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigibody2D();
        LoadMeleeAttack();
        LoadStatsSystem();
        LoadRangedAttack();
        LoadShooting();

        if (movement == null) movement = GetComponentInChildren<IMPlayerMovement>();
    }

    protected override void Start()
    {
        base.Start();
        if (InfiniteMapManager.Instance.MapData.MapCanLoad)
        {
            transform.position = InfiniteMapManager.Instance.MapData.PlayerInfo.PosOffset + InfiniteMapManager.Instance.Map.Maps[0].position;
        }
    }

    private void LoadRigibody2D()
    {
        if (rb2D != null) return;

        rb2D = GetComponent<Rigidbody2D>();
    }

    private void LoadMeleeAttack()
    {
        if (meleeAttack != null) return;

        meleeAttack = GetComponentInChildren<IMPlayerMeleeAttack>();
    }

    private void LoadRangedAttack()
    {
        if (rangedAttack != null) return;

        rangedAttack = GetComponentInChildren<IMPlayerRangedAttack>();
    }

    private void LoadStatsSystem()
    {
        if (statsSystem != null) return;

        statsSystem = GetComponentInChildren<IMPlayerStats>();
    }

    private void LoadShooting()
    {
        if (shooting != null) return;

        shooting = GetComponentInChildren<IMEntityShooting>();
    }
}