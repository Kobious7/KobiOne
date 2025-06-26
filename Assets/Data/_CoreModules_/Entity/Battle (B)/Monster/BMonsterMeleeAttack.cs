using System;
using System.Collections;
using UnityEngine;

public class BMonsterMeleeAttack : BEntityComponent, IEntityMeleeAttack
{
    [SerializeField] protected float range = 1;
    [SerializeField] protected LayerMask layerMask;
    public event Action<BEntityStats, BEntityStats> OnMeleeHitTarget;
    private BMonster monster;
    private BPlayer player;
    private BMonsterAnim anim;
    private BPlayerAnim pAnim;

    protected override void Start()
    {
        base.Start();

        monster = Entity as BMonster;
        player = BattleManager.Instance.Player;
        anim = Entity.Anim as BMonsterAnim;
        pAnim = player.Anim as BPlayerAnim;
    }

    public IEnumerator MeleeAttack()
    {
        yield return StartCoroutine(monster.Movement.MoveToTarget());
        anim.MeleeAttack();
        yield return StartCoroutine(anim.WaitAnim("MeleeAttack"));
        yield return StartCoroutine(monster.Movement.MoveBack());
        MonsterAnimationEvents.Instance.Hit = false;
    }

    public IEnumerator CheckHit()
    {
        Collider[] monsters = Physics.OverlapSphere(monster.CenterPoint.transform.position, range, layerMask);

        Debug.Log("" + monsters.Length);

        if (monsters.Length <= 0) yield return null;

        MonsterAnimationEvents.Instance.Hit = true;
        pAnim.BeingHit();

        OnMeleeHitTarget?.Invoke(monster.Stats, player.Stats);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawSphere(Entity.CenterPoint.transform.position, range);
    // }
}