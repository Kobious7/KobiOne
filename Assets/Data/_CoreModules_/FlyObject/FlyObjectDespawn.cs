using UnityEngine;

public class FlyObjectDespawn : DespawnByDistacne
{
    protected override void ObjectDespawn()
    {
        InfiniteMapManager.Instance.FlyObjectSpawner.Despawn(transform.parent);
    }
}