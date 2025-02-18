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
        [SerializeField] private Transform selectedItem;

        public Transform SelectedItem
        {
            get => selectedItem;
            set => selectedItem = value;
        }

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
            SpawnLegArmorUI();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        public void SpawnLegArmorUI()
        {
            for(int i = 0; i < legArmorList.Count; i++)
            {
                Transform newLegArmor = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newLegArmor.transform.localScale = Vector3.one;
                LegArmorUI legArmorUI = newLegArmor.GetComponent<LegArmorUI>();
                legArmorUI.Index = i;
                legArmorUI.EquipSO = legArmorList[i].EquipSO;

                legArmorUI.ShowLegArmor(legArmorList[i]);
                newLegArmor.gameObject.SetActive(true);
            }
        }
    }
}