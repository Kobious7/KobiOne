using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class BodyArmorUISpawner : Spawner
    {
        private static BodyArmorUISpawner instance;

        public static BodyArmorUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> bodyArmorList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 BodyArmorUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadBodyArmor();

            bodyArmorList = Game.Instance.Inventory.BodyArmorList;
            SpawnBodyArmors();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnBodyArmors()
        {
            foreach(InventoryEquip equipment in bodyArmorList)
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