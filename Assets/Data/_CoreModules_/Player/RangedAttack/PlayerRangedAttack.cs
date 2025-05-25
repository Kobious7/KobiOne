using System.Collections;
using UnityEngine;


public class PlayerRangedAttack : PlayerComponent
{
    [SerializeField] private float range = 1;

    protected virtual void InfiniteMapRangedAttack() { }

    public virtual IEnumerator BattleRangedAttack()
    {
        yield return null;
    }
}
