using System.Collections;
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
        [SerializeField] private Transform selectedItem;

        public Transform SelectedItem
        {
            get => selectedItem;
            set => selectedItem = value;
        }

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 BootsUISpawner is allowed to exist!");

            instance = this;
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(WaitNextFrame());
        }

        private IEnumerator WaitNextFrame()
        {
            yield return null;

            bootsList = Game.Instance.Inventory.BootsList;
            SpawnBootsUI();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        public void SpawnBootsUI()
        {
            for(int i = 0; i < bootsList.Count; i++)
            {
                Transform newBoots = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newBoots.transform.localScale = Vector3.one;
                BootsUI bootsUI = newBoots.GetComponent<BootsUI>();
                bootsUI.Index = i;
                bootsUI.EquipSO = bootsList[i].EquipSO;

                bootsUI.ShowBoots(bootsList[i]);
                newBoots.gameObject.SetActive(true);
            }
        }
    }
}