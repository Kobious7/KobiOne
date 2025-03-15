using UnityEngine;

namespace InfiniteMap
{
    public class EquipmentAb : GMono
    {
        [SerializeField] private Equipment equipment;

        public Equipment Equipment => equipment;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            if(equipment != null) return;

            equipment = transform.parent.GetComponent<Equipment>();
        }
    }
}