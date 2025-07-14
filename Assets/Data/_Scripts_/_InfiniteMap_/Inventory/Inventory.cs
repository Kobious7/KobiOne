using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : GMono
{
    public event Action<InventoryEquip> OnEquipAdding;
    [SerializeField] private int primarionSoul;
    [SerializeField] private int rewardPrimarionSoul;
    [SerializeField] private List<InventoryItem> itemList;
    [SerializeField] private List<InventoryEquip> weaponList;
    [SerializeField] private List<InventoryEquip> helmetList;
    [SerializeField] private List<InventoryEquip> armorList;
    [SerializeField] private List<InventoryEquip> armwearList;
    [SerializeField] private List<InventoryEquip> bootsList;
    [SerializeField] private List<InventoryEquip> specialList;
    [SerializeField] private List<InventoryStuff> rewardItemList;

    #region Inventory element getters and setters
    public int PrimarionSoul
    {
        get => primarionSoul;
        set => primarionSoul = value;
    }

    public int RewardPrimarionSoul
    {
        get => rewardPrimarionSoul;
        set => primarionSoul = value;
    }

    public List<InventoryItem> ItemList
    {
        get => itemList;
        set => itemList = value;
    }
    public List<InventoryEquip> WeaponList
    {
        get => weaponList;
        set => weaponList = value;
    }
    public List<InventoryEquip> HelmetList
    {
        get => helmetList;
        set => helmetList = value;
    }
    public List<InventoryEquip> ArmorList
    {
        get => armorList;
        set => armorList = value;
    }

    public List<InventoryEquip> ArmwearList
    {
        get => armwearList;
        set => armwearList = value;
    }

    public List<InventoryEquip>BootsList
    {
        get => bootsList;
        set => bootsList = value;
    }

    public List<InventoryEquip> SpecialList
    {
        get => specialList;
        set => specialList = value;
    }

    public List<InventoryStuff> RewardItemList
    {
        get => rewardItemList;
        set => rewardItemList = value;
    }
    #endregion

    [SerializeField] private EquipObtainer equipObtainer;

    public EquipObtainer EquipObtainer => equipObtainer;

    [SerializeField] private EquipWearing equipWearing;

    public EquipWearing EquipWearing => equipWearing;

    [SerializeField] private EquipSO test;
    [SerializeField] private EquipSO test1;
    [SerializeField] private EquipSO test2;
    [SerializeField] private EquipSO test3;
    [SerializeField] private EquipSO test4;
    [SerializeField] private EquipSO test5;
    [SerializeField] private EquipSO test6;

    private InfiniteMapManager infiniteMapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEquipObtainer();
        LoadEquipWearing();
    }

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;

        LoadInventoryResources();
        AddEquip(equipObtainer.CreateEquip(test, Rarity.Common));
        AddEquip(equipObtainer.CreateEquip(test1, Rarity.Common));
        // AddEquip(equipObtainer.CreateEquip(test2));
        // AddEquip(equipObtainer.CreateEquip(test3));
        // AddEquip(equipObtainer.CreateEquip(test4));
        // AddEquip(equipObtainer.CreateEquip(test5));
        // AddEquip(equipObtainer.CreateEquip(test6));
        //equipObtainer.UpgradeEquip(weaponList[0], 50);
        // equipObtainer.UpgradeEquip(helmetList[0], 100);
        // equipObtainer.UpgradeEquip(bodyArmorList[0], 100);
        // equipObtainer.UpgradeEquip(legArmorList[0], 100);
        // equipObtainer.UpgradeEquip(bootsList[0], 100);
        // equipObtainer.UpgradeEquip(backItemList[0], 100);
        // equipObtainer.UpgradeEquip(auraList[0], 100);
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

    public void LoadInventoryResources()
    {
        rewardItemList = new();

        LoadPrimarionSoul();
        LoadItem();
        LoadEquip();
    }

    public void LoadPrimarionSoul()
    {
        if (infiniteMapManager.MapData.MapCanLoad && infiniteMapManager.MapData.Result == Result.WIN)
        {
            int monsterLevel = infiniteMapManager.MapData.MonsterInfo.Level;
            rewardPrimarionSoul = UnityEngine.Random.Range(monsterLevel / 2 + monsterLevel, (monsterLevel / 2 + monsterLevel) + monsterLevel * monsterLevel / 100);
            primarionSoul += InfiniteMapManager.Instance.MapData.PrimarionSoul + rewardPrimarionSoul;
        }
    }

    public void LoadItem()
    {
        itemList = infiniteMapManager.MapData.ItemList;
        if (infiniteMapManager.MapData.Result == Result.WIN) GetDropItems();
    }

    public void LoadEquip()
    {
        if(infiniteMapManager.MapData.MapCanLoad)
        {
            weaponList = infiniteMapManager.MapData.WeaponList;
            helmetList = infiniteMapManager.MapData.HelmetList;
            armorList = infiniteMapManager.MapData.ArmorList;
            armwearList = infiniteMapManager.MapData.ArmwearList;
            bootsList = infiniteMapManager.MapData.BootsList;
            specialList = infiniteMapManager.MapData.SpecialList;
        }
        else
        {
            weaponList = new(); //Game.Instance.MapData.WeaponList;
            helmetList = new(); //Game.Instance.MapData.HelmetList;
            armorList = new(); //Game.Instance.MapData.BodyArmorList;
            armwearList = new(); //Game.Instance.MapData.LegArmorList;
            bootsList = new(); //Game.Instance.MapData.BootsList;
            specialList = new(); //Game.Instance.MapData.AuraList;
        }
        if(infiniteMapManager.MapData.Result == Result.WIN) DropEquip();
    }

    public void AddStackableItem(InventoryItem stackItem)
    {
        if (itemList.Count == 0)
        {
            itemList.Add(stackItem);
        }
        else
        {
            bool isNewItem = false;

            foreach (InventoryItem item in itemList)
            {
                if (item.ItemSO.ItemCode == stackItem.ItemSO.ItemCode)
                {
                    item.Quantity = item.Quantity + stackItem.Quantity > item.ItemSO.MaxStack ? item.ItemSO.MaxStack : item.Quantity + stackItem.Quantity;
                    return;
                }

                isNewItem = true;
            }

            if (isNewItem) itemList.Add(stackItem);

            rewardItemList.Add(stackItem);
        } 
    }

    public void AddUnstackableItem(InventoryItem unstackItem)
    {
        itemList.Add(unstackItem);
        rewardItemList.Add(unstackItem);
    }

    public void AddEquip(InventoryEquip equip)
    {
        List<InventoryEquip> equipList = GetEquipListByEquipType(equip.EquipSO.EquipType);
        equipList.Add(equip);
        rewardItemList.Add(equip);
        OnEquipAdding?.Invoke(equip);
    }

    public List<InventoryEquip> GetEquipListByEquipType(EquipType equipType)
    {
        if(equipType == EquipType.Weapon) return weaponList;
        if(equipType == EquipType.Helmet) return helmetList;
        if(equipType == EquipType.Armor) return armorList;
        if(equipType == EquipType.Armwear) return armwearList;
        if(equipType == EquipType.Boots) return bootsList;
        if(equipType == EquipType.Special) return specialList;

        return null;
    }

    public void GetDropItems()
    {
        foreach(ItemSO item in InfiniteMapManager.Instance.MapData.ItemDropList)
        {
            float rate = UnityEngine.Random.Range(0f, 100f);

            if(rate <= item.DropRate)
            {
                if(item.ItemType == ItemType.STACK) AddStackableItem(new InventoryItem(item, 1));
                else if(item.ItemType == ItemType.NOSTACK) AddUnstackableItem(new InventoryItem(item, 1));
            }
        }
    }

    public void DropEquip()
    {   
        float dropRate = UnityEngine.Random.Range(0f, 100f);

        if(dropRate > 30) return;

        RarityCalculator rarityCalculator = new RarityCalculator();
        
        foreach(EquipSO equipSO in InfiniteMapManager.Instance.MapData.EquipDropList)
        {

            Rarity rarity = rarityCalculator.GetNormalMonsterRarity();

            if(rarity == Rarity.None) continue;

            InventoryEquip newEquip = equipObtainer.CreateEquip(equipSO, rarity);
            AddEquip(newEquip);
        }
    }
}