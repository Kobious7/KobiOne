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
        [SerializeField] private Transform selectedItem;

        public Transform SelectedItem
        {
            get => selectedItem;
            set => selectedItem = value;
        }

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
            SpawnBodyArmorUI();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        public void SpawnBodyArmorUI()
        {
            for(int i = 0; i < bodyArmorList.Count; i++)
            {
                Transform newBodyArmor = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newBodyArmor.transform.localScale = Vector3.one;
                BodyArmorUI bodyArmor = newBodyArmor.GetComponent<BodyArmorUI>();
                bodyArmor.Index = i;
                bodyArmor.EquipSO = bodyArmorList[i].EquipSO;

                bodyArmor.ShowWeapon(bodyArmorList[i]);
                newBodyArmor.gameObject.SetActive(true);
            }
        }
    }
}