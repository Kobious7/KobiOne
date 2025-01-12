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

        public void LoadInventory()
        {
            listItems = Game.Instance.MapData.ListItems;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropItems();
        }

        public void LoadWeapons()
        {
            weaponList = Game.Instance.MapData.WeaponList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropWeapons();
        }

        public void LoadHelmets()
        {
            helmetList = Game.Instance.MapData.HelmetList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropHelmets();
        }

        public void LoadBodyArmor()
        {
            bodyArmorList = Game.Instance.MapData.BodyArmorList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropBodyArmors();
        }

        public void LoadLegArmor()
        {
            legArmorList = Game.Instance.MapData.LegArmorList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropLegArmors();
        }

        public void LoadBoots()
        {
            bootsList = Game.Instance.MapData.BootsList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropBoots();
        }

        public void LoadAuras()
        {
            auraList = Game.Instance.MapData.AuraList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropAuras();
        }

        public void LoadBackItems()
        {
            backItemList = Game.Instance.MapData.BackItemList;
            if(Game.Instance.MapData.Result == Result.WIN) GetDropBackItems();
        }

        public void AddItem(InventoryItem stackItem)
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

        public void AddEquip(InventoryEquip equip)
        {
            if(equip.EquipSO.EquipType == EquipType.WEAP) weaponList.Add(equip);
            if(equip.EquipSO.EquipType == EquipType.HELMET) helmetList.Add(equip);
            if(equip.EquipSO.EquipType == EquipType.BODYARMOR) bodyArmorList.Add(equip);
            if(equip.EquipSO.EquipType == EquipType.LEGARMOR) legArmorList.Add(equip);
            if(equip.EquipSO.EquipType == EquipType.BOOTS) bootsList.Add(equip);
            if(equip.EquipSO.EquipType == EquipType.AURA) auraList.Add(equip);
            if(equip.EquipSO.EquipType == EquipType.BACKITEM) backItemList.Add(equip);
        }

        public void GetDropItems()
        {
            foreach(ItemSO item in Game.Instance.MapData.ItemDropList)
            {
                float rate = Random.Range(0f, 1f);

                if(rate <= item.DropRate / 100)
                {
                    AddItem(new InventoryItem(item, 1));
                }
            }
        }

        public void GetDropWeapons()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.WEAP)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }

        public void GetDropHelmets()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.HELMET)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }

        public void GetDropBodyArmors()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.BODYARMOR)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }

        public void GetDropLegArmors()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.LEGARMOR)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }
        
        public void GetDropBoots()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.BOOTS)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }

        public void GetDropAuras()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.AURA)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }

        public void GetDropBackItems()
        {
            foreach(EquipSO equip in Game.Instance.MapData.EquipDropList)
            {
                if(equip.EquipType == EquipType.BACKITEM)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate <= equip.DropRate / 100) AddEquip(new InventoryEquip(equip));
                }
            }
        }
    }
}