using UnityEngine;

public class BPlayer : Entity
{
    [SerializeField] private CapsuleCollider capsuleCollider;

    public CapsuleCollider CapsuleCollider => capsuleCollider;

    [SerializeField] private BPlayerMovement movement;

    public BPlayerMovement Movement => movement;

    [SerializeField] private BPlayerMeleeAttack meleeAttack;

    public BPlayerMeleeAttack MeleeAttack => meleeAttack;

    [SerializeField] private BPlayerRangedAttack rangedAttack;

    public BPlayerRangedAttack RangedAttack => rangedAttack;

    [SerializeField] private BPlayerAttackRange attackRange;

    public BPlayerAttackRange AttackRange => attackRange;

    [SerializeField] private BEntityShooting shooting;

    public BEntityShooting Shooting => shooting;

    [SerializeField] private BPlayerStats stats;

    public BPlayerStats Stats => stats;

    [SerializeField] private BPlayerSwordrain swordrain;

    public BPlayerSwordrain Swordrain => swordrain;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCapsuleCollider();
        LoadMovement();
        LoadMeleeAttack();
        LoadRangedAttack();
        LoadAttackRange();
        LoadShooting();
        LoadStats();
        LoadSwordrain();
    }

    private void LoadCapsuleCollider()
    {
        if (capsuleCollider != null) return;

        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void LoadMovement()
    {
        if (movement != null) return;

        movement = GetComponentInChildren<BPlayerMovement>();
    }

    private void LoadMeleeAttack()
    {
        if (meleeAttack != null) return;

        meleeAttack = GetComponentInChildren<BPlayerMeleeAttack>();
    }

    private void LoadRangedAttack()
    {
        if (rangedAttack != null) return;

        rangedAttack = GetComponentInChildren<BPlayerRangedAttack>();
    }

    private void LoadAttackRange()
    {
        if (attackRange != null) return;

        attackRange = GetComponentInChildren<BPlayerAttackRange>();
    }

    private void LoadShooting()
    {
        if (shooting != null) return;

        shooting = GetComponentInChildren<BEntityShooting>();
    }

    private void LoadStats()
    {
        if (stats != null) return;

        stats = GetComponentInChildren<BPlayerStats>();
    }

    private void LoadSwordrain()
    {
        if (swordrain != null) return;

        swordrain = GetComponentInChildren<BPlayerSwordrain>();
    }
}