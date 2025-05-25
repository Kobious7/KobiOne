using System.Collections;
using UnityEngine;

public class OpponentAttack : OpponentComponent
{
    [SerializeField] private LayerMask mask;

    public IEnumerator MeleeAttack()
    {
        Opponent.Anim.DealSlashAnim();
        yield return new WaitForSeconds(Opponent.Anim.GetAnimDuration("MeleeAttackAnim"));
    }

    public virtual void RangeAttack()
    {

    }
}