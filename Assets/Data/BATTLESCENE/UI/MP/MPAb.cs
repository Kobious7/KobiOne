using UnityEngine;

namespace Battle
{
    public class MPAb : GMono
    {
        [SerializeField] private MP mPAbs;

        public MP MPAbs => mPAbs;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadMP();
        }

        private void LoadMP()
        {
            if (mPAbs != null) return;

            mPAbs = transform.parent.GetComponent<MP>();
        }
    }
}