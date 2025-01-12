using UnityEngine;

namespace Battle
{
    public class FlyingObjCollision : FlyingObjAb
    {
        private void OnTriggerEnter(Collider other)
        {
            TilePrefab tile = other.transform.parent.GetComponentInChildren<TilePrefab>();
            TilePrefab target = FlyingObj.Target.GetComponentInChildren<TilePrefab>();

            if (other.transform.parent.name == "Tile")
            {
                if (tile.X == target.X && tile.Y == target.Y)
                {
                    Game.Instance.SkillSpawner.Despawn(transform.parent);
                    Game.Instance.SkillSpawner.SpawnedCount--;
                }
            }
        }
    }
}