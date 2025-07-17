using System.Collections;
using UnityEngine;

public class BMonsterRangedAttack : BEntityComponent
{
    private BMonsterAnim anim;

    protected override void Start()
    {
        base.Start();

        anim = Entity.Anim as BMonsterAnim;
    }

    public IEnumerator RangedAttack()
    {
        anim.RangedAttack();

        yield return StartCoroutine(anim.WaitAnim("SwordRangedAttack"));

        while (BattleManager.Instance.FlyObjectSpawner.ActiveCount > 0)
        {
            yield return null;
        }

        anim.Idle();

        yield return StartCoroutine(anim.WaitAnim("Idle"));
    }
}