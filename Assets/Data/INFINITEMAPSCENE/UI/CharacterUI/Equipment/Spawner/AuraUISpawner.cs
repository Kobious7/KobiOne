using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class AuraUISpawner : Spawner
    {
        private static AuraUISpawner instance;

        public static AuraUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> auraList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 AuraUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadAuras();

            auraList = Game.Instance.Inventory.AuraList;
            SpawnAuras();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnAuras()
        {
            foreach(InventoryEquip equipment in auraList)
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