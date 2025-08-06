using System.Collections;
using UnityEngine;

public class FlyObjectInfiniteMapCollision : FlyObjectCollision
{
    protected override void Collide(Collider other)
    {
        base.Collide(other);

        StartCoroutine(MonsterCheckHit(other));
    }

    private IEnumerator MonsterCheckHit(Collider other)
    {
        IMMonster monsterCom = other.transform.parent.GetComponent<IMMonster>();

        if (monsterCom != null && monsterCom is IMMonster)
        {
            IMMonsterAnim anim = monsterCom.Anim as IMMonsterAnim;
            anim.BeingHit();
            monsterCom.IsBeingHit = true;

            yield return null;

            InfiniteMapManager.Instance.Player.CanLockMovement = true;
            InfiniteMapManager.Instance.Player.CallOnBattlePreparingEvent(monsterCom);
        }

        InfiniteMapManager.Instance.FlyObjectSpawner.Despawn(transform.parent);
    }
}