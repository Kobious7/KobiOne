using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class HelmetUISpawner : Spawner
    {
        private static HelmetUISpawner instance;

        public static HelmetUISpawner Instance => instance;

        [SerializeField] private List<InventoryEquip> helmetList;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 HelmetUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.LoadHelmets();

            helmetList = Game.Instance.Inventory.HelmetList;
            SpawnHelmets();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        private void SpawnHelmets()
        {
            foreach(InventoryEquip helmet in helmetList)
            {
                Transform newHel = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newHel.transform.localScale = Vector3.one;
                EquipmentItemUI equip = newHel.GetComponent<EquipmentItemUI>();
                equip.Model.sprite = helmet.EquipSO.Sprite;

                newHel.gameObject.SetActive(true);
            }
        }
    }
}