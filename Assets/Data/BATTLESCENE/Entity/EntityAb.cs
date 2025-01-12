using UnityEngine;

namespace Battle
{
    public abstract class EntityAb : GMono
    {
        [SerializeField] private Entity entity;

        public Entity Entity => entity;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadEntity();
        }

        private void LoadEntity()
        {
            if (entity != null) return;

            entity = transform.parent.GetComponent<Entity>();
        }
    }
}