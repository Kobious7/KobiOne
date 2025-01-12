using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class WeaponUISpawner : Spawner
    {
        private static WeaponUISpawner instance;

        public static WeaponUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> weaponList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 WeaponUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadWeapons();

            weaponList = Game.Instance.Inventory.WeaponList;
            SpawnWeapons();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnWeapons()
        {
            foreach(InventoryEquip weapon in weaponList)
            {
                Transform newWeap = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newWeap.transform.localScale = Vector3.one;
                EquipmentItemUI equip = newWeap.GetComponent<EquipmentItemUI>();
                equip.Model.sprite = weapon.EquipSO.Sprite;

                newWeap.gameObject.SetActive(true);
            }
        }
    }
}