using UnityEngine;

public class FlyObjectDespawn : DespawnByDistacne
{
    protected override void ObjectDespawn()
    {
        Game.Instance.FlyObjectSpawner.Despawn(transform.parent);
    }
}