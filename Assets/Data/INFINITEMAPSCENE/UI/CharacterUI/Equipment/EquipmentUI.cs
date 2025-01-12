using UnityEngine;

namespace InfiniteMap
{
    public class EquipmentUI : GMono
    {
        private static EquipmentUI instance;

        public static EquipmentUI Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 EquipmentUI is allowed to exist!");

            instance = this;
        }
    }
}