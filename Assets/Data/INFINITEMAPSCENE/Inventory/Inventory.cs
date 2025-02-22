using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteMap
{
    public class Inventory : GMono
    {
        [SerializeField] private List<InventoryItem> listItems;

        public List<InventoryItem> ListItems
        {
            get => listItems;
            set => listItems = value;
        }

        [SerializeField] private List<InventoryEquip> weaponList;

        public List<InventoryEquip> WeaponList
        {
            get => weaponList;
            set => weaponList = value;
        }

        [SerializeField] private List<InventoryEquip> helmetList;

        public List<InventoryEquip> HelmetList
        {
            get => helmetList;
            set => helmetList = value;
        }

        [SerializeField] private List<InventoryEquip> bodyArmorList;

        public List<InventoryEquip> BodyArmorList
        {
            get => bodyArmorList;
            set => bodyArmorList = value;
        }

        [SerializeField] private List<InventoryEquip> legArmorList;

        public List<InventoryEquip> LegArmorList
        {
            get => legArmorList;
            set => legArmorList = value;
        }

        [SerializeField] private List<InventoryEquip> bootsList;

        public List<InventoryEquip>BootsList
        {
            get => bootsList;
            set => bootsList = value;
        }

        [SerializeField] private List<InventoryEquip> auraList;

        public List<InventoryEquip> AuraList
        {
            get => auraList;
            set => auraList = value;
        }

        [SerializeField] private List<InventoryEquip> backItemList;

        public List<InventoryEquip> BackItemList
        {
            get => backItemList;
            set => backItemList = value;
        }

        [SerializeField] private EquipObtainer equipObtainer;

        public EquipObtainer EquipObtainer => equipObtainer;

        [SerializeField] private EquipWearing equipWearing;

        public EquipWearing EquipWearing => equipWearing;

        [SerializeField] private EquipSO test;
        [SerializeField] private EquipSO test1;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadEquipObtainer();
            LoadEquipWearing();
        }

        protected override void Start()
        {
            base.Start();
            AddEquip(equipObtainer.CreateEquip(test));
            AddEquip(equipObtainer.CreateEquip(test1));
            equipObtainer.UpgradeEquip(weaponList[0], 100);
            equipObtainer.UpgradeEquip(helmetList[0], 100);
            equipWearing.Wear(weaponList[0]);
            equipWearing.Wear(helmetList[0]);
        }

        private void LoadEquipObtainer()
        {
            if(equipObtainer != null) return;

            equipObtainer = GetComponentInChildren<EquipObtainer>();
        }

        private void LoadEquipWearing()
        {
            if(equipWearing != null) return;

            equipWearing = GetComponentInChildren<EquipWearing>();
        }

        public void LoadInventory()
        {
            listItems = Game.Instance.MapData.ListItems;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropItems();
        }

        public void LoadEquip()
        {
            weaponList = Game.Instance.MapData.WeaponList;
            helmetList = Game.Instance.MapData.HelmetList;
            bodyArmorList = Game.Instance.MapData.BodyArmorList;
            legArmorList = Game.Instance.MapData.LegArmorList;
            bootsList = Game.Instance.MapData.BootsList;
            auraList = Game.Instance.MapData.AuraList;
            backItemList = Game.Instance.MapData.BackItemList;
            if(Game.Instance.MapData.Result == Result.WIN) DropEquip();
        }

        public void AddStackableItem(InventoryItem stackItem)
        {

            if(listItems.Count == 0)
            {
                listItems.Add(stackItem);
            }
            else
            {
                bool isNewItem = false;

                foreach(InventoryItem item in listItems)
                {
                    if(item.ItemSO.ItemCode == stackItem.ItemSO.ItemCode)
                    {
                        item.Quantity = item.Quantity + stackItem.Quantity > item.ItemSO.MaxStack ? item.ItemSO.MaxStack : item.Quantity + stackItem.Quantity;
                        return;
                    }

                    isNewItem = true;
                }

                if(isNewItem) listItems.Add(stackItem);
            } 
        }

        public void AddUnstackableItem(InventoryItem unstackableItem)
        {
            listItems.Add(unstackableItem);
        }

        public void AddEquip(InventoryEquip equip)
        {
            List<InventoryEquip> equipList = GetEquipListByEquipType(equip.EquipSO.EquipType);
            equipList.Add(equip);
        }

        public List<InventoryEquip> GetEquipListByEquipType(EquipType equipType)
        {
            if(equipType == EquipType.WEAP) return weaponList;
            if(equipType == EquipType.HELMET) return helmetList;
            if(equipType == EquipType.BODYARMOR) return bodyArmorList;
            if(equipType == EquipType.LEGARMOR) return legArmorList;
            if(equipType == EquipType.BOOTS) return bootsList;
            if(equipType == EquipType.AURA) return auraList;
            if(equipType == EquipType.BACKITEM) return backItemList;

            return null;
        }

        public void GetDropItems()
        {
            foreach(ItemSO item in Game.Instance.MapData.ItemDropList)
            {
                float rate = Random.Range(0f, 1f);

                if(rate <= item.DropRate / 100)
                {
                    if(item.ItemType == ItemType.STACK) AddStackableItem(new InventoryItem(item, 1));
                    else if(item.ItemType == ItemType.NOSTACK) AddUnstackableItem(new InventoryItem(item, 1));
                }
            }
        }

        public void DropEquip()
        {
            foreach(EquipSO equipSO in Game.Instance.MapData.EquipDropList)
            {
                InventoryEquip newEquip = equipObtainer.CreateEquip(equipSO);

                if(newEquip == null) continue;

                AddEquip(newEquip);
            }
        }
    }
}