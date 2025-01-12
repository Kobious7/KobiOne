using UnityEngine;

namespace InfiniteMap
{
    public abstract class FlyObjectAb : GMono
    {
        [SerializeField] private FlyObject flyObject;

        public FlyObject FlyObject => flyObject;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadFlyObject();
        }

        private void LoadFlyObject()
        {
            if (flyObject != null) return;

            flyObject = transform.parent.GetComponent<FlyObject>();
        }
    }
}