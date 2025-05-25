using System;
using System.Collections;
using UnityEngine;

public class PlayerBatlleMeleeAttack : PlayerMeleeAttack
{
    public event Action OnMeleeHitTarget;

    public override IEnumerator BattleMeleeAttack()
    {
        yield return StartCoroutine(Player.BattleMovement.MoveToTarget());
        Player.Anim.MeleeAttack();
        yield return StartCoroutine(Player.Anim.WaitAnim("SwordMeleeAttack"));
        yield return StartCoroutine(Player.BattleMovement.MoveBack());
        PlayerEventsInAnim.Instance.Hit = false;
    }

    public override IEnumerator CheckHit()
    {
        Collider[] monsters = Physics.OverlapSphere(Player.CenterPoint.transform.position, range, layerMask);

        Debug.Log("" + monsters.Length);

        if (monsters.Length <= 0) yield return null;

        PlayerEventsInAnim.Instance.Hit = true;

        OnMeleeHitTarget?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(Player.CenterPoint.transform.position, range);
    }
}