using UnityEngine;

public class FlyObjectInfiniteMapCollision : FlyObjectCollision
{
    protected override void Collide(Collider other)
    {
        base.Collide(other);

        Debug.Log(other.transform.parent.name);
        
        IMMonster monsterCom = other.transform.parent.GetComponent<IMMonster>();
        
        if (monsterCom != null && monsterCom is IMMonster)
        {
            InfiniteMapManager.Instance.Player.CallOnBattlePreparingEvent(monsterCom);
        }

        InfiniteMapManager.Instance.FlyObjectSpawner.Despawn(transform.parent);
    }
}