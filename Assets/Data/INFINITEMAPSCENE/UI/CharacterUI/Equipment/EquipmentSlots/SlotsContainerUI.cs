using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfiniteMap
{
    public class SlotContainerUI : GMono
    {
        [SerializeField] private List<EquipSlotUI> slots;

        public List<EquipSlotUI> Slots => slots;

        [SerializeField] private List<Spawner> spawners;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSlots();
            LoadSpawners();
        }

        protected override void Start()
        {
            base.Start();
            LoadListener();
        }

        private void LoadSlots()
        {
            if(slots.Count > 0) return;

            slots = GetComponentsInChildren<EquipSlotUI>().ToList();
        }

        private void LoadSpawners()
        {
            if(spawners.Count > 0) return;

            spawners = transform.parent.GetComponentsInChildren<Spawner>().ToList();
        }

        private void LoadListener()
        {
            for(int i = 0; i < slots.Count; i++)
            {
                int index = i;
                slots[i].Btn.onClick.AddListener(() => Click(index));
            }
        }

        private void Click(int index)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if(i == index)
                {
                    if(!slots[i].OnSelected.gameObject.activeSelf) slots[i].OnSelected.gameObject.SetActive(true);

                    if(index <= 1)
                    {
                        if(!spawners[0].gameObject.activeSelf) spawners[0].gameObject.SetActive(true);
                        for(int j = 1; j < spawners.Count; j++)
                        {
                            if(spawners[j].gameObject.activeSelf) spawners[j].gameObject.SetActive(false);
                        }                
                    }
                    else
                    {
                        for(int j = 0; j < spawners.Count; j++)
                        {
                            if(index - 1 == j)
                            {
                                if(!spawners[j].gameObject.activeSelf) spawners[j].gameObject.SetActive(true);
                            }
                            else
                            {
                                if(spawners[j].gameObject.activeSelf) spawners[j].gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    if(slots[i].OnSelected.gameObject.activeSelf) slots[i].OnSelected.gameObject.SetActive(false);
                }
            }
        }
    }
}