using System;
using System.Collections;
using UnityEngine;

public class BPlayerMeleeAttack : EntityComponent, IEntityMeleeAttack
{
    [SerializeField] protected float range = 1;
    [SerializeField] protected LayerMask layerMask;
    public event Action<BEntityStats, BEntityStats> OnMeleeHitTarget;
    private BPlayer player;
    private BMonster monster;
    private BPlayerAnim anim;
    private BMonsterAnim mAnim;

    protected override void Start()
    {
        base.Start();

        player = Entity as BPlayer;
        monster = BattleManager.Instance.Monster;
        anim = Entity.Anim as BPlayerAnim;
        mAnim = monster.Anim as BMonsterAnim;
    }

    public IEnumerator MeleeAttack()
    {
        yield return StartCoroutine(player.Movement.MoveToTarget());
        anim.MeleeAttack();
        yield return StartCoroutine(anim.WaitAnim("SwordMeleeAttack"));
        yield return StartCoroutine(player.Movement.MoveBack());
        PlayerAnimationEvents.Instance.Hit = false;
    }

    public IEnumerator CheckHit()
    {
        Collider[] monsters = Physics.OverlapSphere(player.CenterPoint.transform.position, range, layerMask);

        Debug.Log("" + monsters.Length);

        if (monsters.Length <= 0) yield return null;

        PlayerAnimationEvents.Instance.Hit = true;
        mAnim.BeingHit();

        OnMeleeHitTarget?.Invoke(player.Stats, monster.Stats);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(Entity.CenterPoint.transform.position, range);
    }
}