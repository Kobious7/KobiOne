using System;
using System.Collections;
using UnityEngine;

public class PlayerMeleeAttack : PlayerComponent
{
    [SerializeField] protected float range = 1;
    [SerializeField] protected LayerMask layerMask;

    protected virtual void InfiniteMapMeleeAttack()
    {
        //Override
    }

    public virtual IEnumerator BattleMeleeAttack()
    {
        yield return null;
    }

    public virtual IEnumerator CheckHit()
    {
        yield return null;
    }
}