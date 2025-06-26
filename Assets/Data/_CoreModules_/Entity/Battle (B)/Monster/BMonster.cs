using UnityEngine;

public class BMonster : BEntity
{
    [SerializeField] private BMonsterDestroyAndFill destroyAndFill;

    public BMonsterDestroyAndFill DestroyAndFill => destroyAndFill;

    [SerializeField] private BMonsterMoveTile moveTile;

    public BMonsterMoveTile MoveTile => moveTile;

    [SerializeField] private BMonsterPick pick;

    public BMonsterPick Pick => pick;

    [SerializeField] private BMonsterMeleeAttack meleeAttack;

    public BMonsterMeleeAttack MeleeAttack => meleeAttack;

    [SerializeField] private BMonsterRangedAttack rangedAttack;

    public BMonsterRangedAttack RangedAttack => rangedAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDestroyAndFill();
        LoadMoveTile();
        LoadPick();
        LoadMeleeAttack();
        LoadRangedAttack();
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
}