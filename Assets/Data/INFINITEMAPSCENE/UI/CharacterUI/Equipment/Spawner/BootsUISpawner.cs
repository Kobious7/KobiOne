using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class BootsUISpawner : Spawner
    {
        private static BootsUISpawner instance;

        public static BootsUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> bootsList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 BootsUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadBoots();

            bootsList = Game.Instance.Inventory.BootsList;
            SpawnBoots();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnBoots()
        {
            foreach(InventoryEquip equipment in bootsList)
            {
                Transform newEquip = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newEquip.transform.localScale = Vector3.one;
                EquipmentItemUI equip = newEquip.GetComponent<EquipmentItemUI>();
                equip.Model.sprite = equipment.EquipSO.Sprite;

                newEquip.gameObject.SetActive(true);
            }
        }
    }
}