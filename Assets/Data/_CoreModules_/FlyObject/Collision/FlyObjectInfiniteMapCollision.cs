using UnityEngine;

public class FlyObjectInfiniteMapCollision : FlyObjectCollision
{
    protected override void Collide(Collider other)
    {
        base.Collide(other);
        
        InfiniteMapManager.Instance.FlyObjectSpawner.Despawn(transform.parent);

        if (other.transform.parent.name == "Monster")
        {
            InfiniteMapManager.Instance.LoadDataToInfiniteMapSO(other.transform);
            LoadScene(BATTLE);
        }
    }
}