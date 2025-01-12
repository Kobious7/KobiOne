using UnityEngine;

namespace InfiniteMap
{
    public abstract class DespawnByDistacne : GMono
    {
        [SerializeField] private float distance = 50f;
        [SerializeField] private Transform target;

        private void Update()
        {
            Despawn();
        }

        private void Despawn()
        {
            float currentDist = Vector3.Distance(transform.parent.position, target.position);

            if (currentDist <= distance) return;

            SpawnerDespawn();
        }

        protected abstract void SpawnerDespawn();
    }
}