using System;
using UnityEngine;

public class IMPlayer : Entity
{
    public event Action<IMMonster> OnBattlePreparing;
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

    [SerializeField] private bool canLockMovement = false;

    public bool CanLockMovement { get => canLockMovement; set => canLockMovement = value; }

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

    public void CallOnBattlePreparingEvent(IMMonster monster)
    {
        OnBattlePreparing?.Invoke(monster);
    }
}