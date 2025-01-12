using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class BackItemUISpawner : Spawner
    {
        private static BackItemUISpawner instance;

        public static BackItemUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> backItemList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 BackItemUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadBackItems();

            backItemList = Game.Instance.Inventory.BackItemList;
            SpawnBackItems();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnBackItems()
        {
            foreach(InventoryEquip equipment in backItemList)
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