using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class LegArmorUISpawner : Spawner
    {
        private static LegArmorUISpawner instance;

        public static LegArmorUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> legArmorList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 LegArmorUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadLegArmor();

            legArmorList = Game.Instance.Inventory.LegArmorList;
            SpawnLegArmors();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnLegArmors()
        {
            foreach(InventoryEquip equipment in legArmorList)
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