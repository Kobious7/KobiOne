using UnityEngine;

namespace InfiniteMap
{
    public class FlyObjectDespawn : DespawnByDistacne
    {
        protected override void SpawnerDespawn()
        {
            Game.Instance.FlyObjectSpawner.Despawn(transform.parent);
        }
    }
}