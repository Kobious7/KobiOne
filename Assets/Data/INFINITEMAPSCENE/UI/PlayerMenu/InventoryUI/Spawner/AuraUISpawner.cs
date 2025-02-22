using System.Collections;
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
        [SerializeField] private Transform selectedItem;

        public Transform SelectedItem
        {
            get => selectedItem;
            set => selectedItem = value;
        }

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 AuraUISpawner is allowed to exist!");

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

            auraList = Game.Instance.Inventory.AuraList;
            SpawnAuraUI();
        }

        protected override void LoadHolder()
        {
            if(holder != null) return;

            holder = GetComponent<ScrollRect>().content;
        }

        public void SpawnAuraUI()
        {
            for(int i = 0; i < auraList.Count; i++)
            {
                Transform newAura = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
                newAura.transform.localScale = Vector3.one;
                AuraUI auraUI = newAura.GetComponent<AuraUI>();
                auraUI.Index = i;
                auraUI.EquipSO = auraList[i].EquipSO;

                auraUI.ShowAura(auraList[i]);
                newAura.gameObject.SetActive(true);
            }
        }
    }
}