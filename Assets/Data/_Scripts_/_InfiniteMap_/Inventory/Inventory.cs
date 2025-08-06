using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : GMono
{
    public event Action<InventoryEquip> OnEquipAdding;
    public event Action OnPrimarionSoulChanged;
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
    [SerializeField] private EquipObtainer equipObtainer;
    [SerializeField] private EquipWearing equipWearing;
    [SerializeField] private Soulize soulize;

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

    public List<InventoryEquip> BootsList
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

    public EquipObtainer EquipObtainer => equipObtainer;
    public EquipWearing EquipWearing => equipWearing;
    public Soulize Soulize => soulize;
    #endregion

    [SerializeField] private EquipSO test;
    [SerializeField] private EquipSO test1;

    private InfiniteMapManager infiniteMapManager;
    private PlayerSO playerData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEquipObtainer();
        LoadEquipWearing();

        if (soulize == null) soulize = GetComponentInChildren<Soulize>();
    }

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;
        playerData = infiniteMapManager.PlayerData;

        LoadInventoryResources();
        // AddEquip(equipObtainer.CreateEquip(test, Rarity.Common));
        // AddEquip(equipObtainer.CreateEquip(test1, Rarity.Common));
    }

    private void LoadEquipObtainer()
    {
        if (equipObtainer != null) return;

        equipObtainer = GetComponentInChildren<EquipObtainer>();
    }

    private void LoadEquipWearing()
    {
        if (equipWearing != null) return;

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
        if (infiniteMapManager.MapData.Result == Result.WIN)
        {
            int monsterLevel = infiniteMapManager.MapData.MonsterInfo.Level;
            rewardPrimarionSoul = UnityEngine.Random.Range(monsterLevel / 2 + monsterLevel, (monsterLevel / 2 + monsterLevel) + monsterLevel * monsterLevel / 100);
            Debug.Log(rewardPrimarionSoul);
            primarionSoul += playerData.PrimarionSoul + rewardPrimarionSoul;

            //Save data
            playerData.PrimarionSoul = primarionSoul;
        }
        else
        {
            primarionSoul = playerData.PrimarionSoul;
        }
    }

    public void LoadItem()
    {
        itemList = playerData.ItemList;
        if (infiniteMapManager.MapData.Result == Result.WIN) GetDropItems();
    }

    public void LoadEquip()
    {
        weaponList = playerData.WeaponList;
        helmetList = playerData.HelmetList;
        armorList = playerData.ArmorList;
        armwearList = playerData.ArmwearList;
        bootsList = playerData.BootsList;
        specialList = playerData.SpecialList;

        if (infiniteMapManager.MapData.Result == Result.WIN) DropEquip();
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

        //Save Data
        switch (equip.EquipSO.EquipType)
        {
            case EquipType.Weapon:
                playerData.WeaponList = equipList;
                break;
            case EquipType.Helmet:
                playerData.HelmetList = equipList;
                break;
            case EquipType.Armor:
                playerData.ArmorList = equipList;
                break;
            case EquipType.Armwear:
                playerData.ArmwearList = equipList;
                break;
            case EquipType.Boots:
                playerData.BootsList = equipList;
                break;
            case EquipType.Special:
                playerData.SpecialList = equipList;
                break;
        }
    }

    public List<InventoryEquip> GetEquipListByEquipType(EquipType equipType)
    {
        if (equipType == EquipType.Weapon) return weaponList;
        if (equipType == EquipType.Helmet) return helmetList;
        if (equipType == EquipType.Armor) return armorList;
        if (equipType == EquipType.Armwear) return armwearList;
        if (equipType == EquipType.Boots) return bootsList;
        if (equipType == EquipType.Special) return specialList;

        return null;
    }

    public void GetDropItems()
    {
        int currentQuantity = itemList.Count;

        foreach (ItemSO item in InfiniteMapManager.Instance.MapData.ItemDropList)
        {
            float rate = UnityEngine.Random.Range(0f, 100f);

            if (rate <= item.DropRate)
            {
                if (item.ItemType == ItemType.STACK) AddStackableItem(new InventoryItem(item, 1));
                else if (item.ItemType == ItemType.NOSTACK) AddUnstackableItem(new InventoryItem(item, 1));
            }
        }

        //Sava Data
        if (currentQuantity < itemList.Count) playerData.ItemList = itemList;
    }

    public void DropEquip()
    {
        float dropRate = UnityEngine.Random.Range(0f, 100f);

        if (dropRate > 30) return;

        RarityCalculator rarityCalculator = new RarityCalculator();

        foreach (EquipSO equipSO in InfiniteMapManager.Instance.MapData.EquipDropList)
        {
            Rarity rarity = rarityCalculator.GetNormalMonsterRarity();

            if (rarity == Rarity.None) continue;

            InventoryEquip newEquip = equipObtainer.CreateEquip(equipSO, rarity);
            AddEquip(newEquip);
        }
    }

    public void CallPrimarionSoulChangedEvent()
    {
        OnPrimarionSoulChanged?.Invoke();
    }

    public void IncreasePrimarionSoul(int amount)
    {
        primarionSoul = primarionSoul + amount > int.MaxValue ? int.MaxValue : primarionSoul += amount;

        OnPrimarionSoulChanged?.Invoke();

        //Save Data
        playerData.PrimarionSoul = primarionSoul;
    }

    public void DescreasePrimarionSoul(int amount)
    {
        primarionSoul = primarionSoul - amount < 0 ? 0 : primarionSoul -= amount;

        OnPrimarionSoulChanged?.Invoke();

        //Save Data
        playerData.PrimarionSoul = primarionSoul;
    }
}