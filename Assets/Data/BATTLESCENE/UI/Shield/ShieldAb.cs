using UnityEngine;

namespace Battle
{
    public class ShieldAb : GMono
    {
        [SerializeField] private Shield shield;

        public Shield Shield => shield;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadShield();
        }

        private void LoadShield()
        {
            if (shield != null) return;

            shield = transform.parent.GetComponent<Shield>();
        }
    }
}