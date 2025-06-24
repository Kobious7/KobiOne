using UnityEngine;

public class BMonster : Entity
{
    [SerializeField] private CapsuleCollider capsuleCollider;

    public CapsuleCollider CapsuleCollider => capsuleCollider;

    [SerializeField] private BMonsterDestroyAndFill destroyAndFill;

    public BMonsterDestroyAndFill DestroyAndFill => destroyAndFill;

    [SerializeField] private BMonsterMoveTile moveTile;

    public BMonsterMoveTile MoveTile => moveTile;

    [SerializeField] private BMonsterPick pick;

    public BMonsterPick Pick => pick;

    [SerializeField] private BMonsterStats stats;

    public BMonsterStats Stats => stats;

    [SerializeField] private BMonsterMovement movement;

    public BMonsterMovement Movement => movement;

    [SerializeField] private BMonsterMeleeAttack meleeAttack;

    public BMonsterMeleeAttack MeleeAttack => meleeAttack;

    [SerializeField] private BMonsterRangedAttack rangedAttack;

    public BMonsterRangedAttack RangedAttack => rangedAttack;

    [SerializeField] private BEntityShooting shooting;

    public BEntityShooting Shooting => shooting;

    [SerializeField] private BMonsterAttackRange attackRange;

    public BMonsterAttackRange AttackRange => attackRange;

    [SerializeField] private BMonsterSwordrain swordrain;

    public BMonsterSwordrain Swordrain => swordrain;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCapsuleCollider();
        LoadDestroyAndFill();
        LoadMoveTile();
        LoadPick();
        LoadStats();
        LoadMovement();
        LoadMeleeAttack();
        LoadRangedAttack();
        LoadShooting();
        LoadAttackRange();
        LoadSwordrain();
    }

    private void LoadCapsuleCollider()
    {
        if (capsuleCollider != null) return;

        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void LoadDestroyAndFill()
    {
        if (destroyAndFill != null) return;

        destroyAndFill = GetComponentInChildren<BMonsterDestroyAndFill>();
    }

    private void LoadMoveTile()
    {
        if (moveTile != null) return;

        moveTile = GetComponentInChildren<BMonsterMoveTile>();
    }

    private void LoadPick()
    {
        if (pick != null) return;

        pick = GetComponentInChildren<BMonsterPick>();
    }

    private void LoadStats()
    {
        if (stats != null) return;

        stats = GetComponentInChildren<BMonsterStats>();
    }

    private void LoadMovement()
    {
        if (movement != null) return;

        movement = GetComponentInChildren<BMonsterMovement>();
    }

    private void LoadMeleeAttack()
    {
        if (meleeAttack != null) return;

        meleeAttack = GetComponentInChildren<BMonsterMeleeAttack>();
    }

    private void LoadRangedAttack()
    {
        if (rangedAttack != null) return;

        rangedAttack = GetComponentInChildren<BMonsterRangedAttack>();
    }

    private void LoadShooting()
    {
        if (shooting != null) return;

        shooting = GetComponentInChildren<BEntityShooting>();
    }

    private void LoadAttackRange()
    {
        if (attackRange != null) return;

        attackRange = GetComponentInChildren<BMonsterAttackRange>();
    }

    private void LoadSwordrain()
    {
        if (swordrain != null) return;

        swordrain = GetComponentInChildren<BMonsterSwordrain>();
    }
}