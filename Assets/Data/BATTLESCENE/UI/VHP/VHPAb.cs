using UnityEngine;

namespace Battle
{
    public class VHPAb : GMono
    {
        [SerializeField] private VHP vHP;

        public VHP VHP => vHP;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadVHP();
        }

        private void LoadVHP()
        {
            if (vHP != null) return;

            vHP = transform.parent.GetComponent<VHP>();
        }
    }
}