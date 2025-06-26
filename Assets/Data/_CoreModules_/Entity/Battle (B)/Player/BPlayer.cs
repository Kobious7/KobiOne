using UnityEngine;

public class BPlayer : BEntity
{
    [SerializeField] private BPlayerMeleeAttack meleeAttack;

    public BPlayerMeleeAttack MeleeAttack => meleeAttack;

    [SerializeField] private BPlayerRangedAttack rangedAttack;

    public BPlayerRangedAttack RangedAttack => rangedAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMeleeAttack();
        LoadRangedAttack();
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
}