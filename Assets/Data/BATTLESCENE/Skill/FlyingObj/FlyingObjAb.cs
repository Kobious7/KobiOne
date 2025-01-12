using UnityEngine;

namespace Battle
{
    public class FlyingObjAb : GMono
    {
        [SerializeField] private FlyingObj flyingObj;

        public FlyingObj FlyingObj => flyingObj;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadFlyingObj();
        }

        private void LoadFlyingObj()
        {
            if (flyingObj != null) return;

            flyingObj = transform.parent.GetComponent<FlyingObj>();
        }
    }
}