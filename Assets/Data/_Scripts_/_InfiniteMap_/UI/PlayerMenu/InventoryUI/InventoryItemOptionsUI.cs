using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemOptionsUI : GMono
{
    [SerializeField] private List<RectTransform> options;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<InventoryItemUISpawner> spawners;
    [SerializeField] private int currentOption;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        options = GetComponentsInChildren<RectTransform>()
                .Where(t => t != transform)
                .ToList();
        buttons = GetComponentsInChildren<Button>().ToList();
        if (spawners.Count <= 0) spawners = transform.parent.GetComponentsInChildren<InventoryItemUISpawner>().ToList();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        InfiniteMapManager.Instance.Inventory.OnEquipAdding += UpdateEquipSpawner;
        InfiniteMapManager.Instance.Inventory.EquipWearing.OnEquipWearing += DespawnEquip;
        InfiniteMapManager.Instance.Equipment.Unequip.OnEquipDisarming += UpdateEquipSpawner;
        InfiniteMapManager.Instance.Inventory.Soulize.OnEquipSoulized += DespawnEquip;
        NewAndLockEquip.Instance.OnNewOrLockChanged += UpdateNewLockItem;
        AddClickListeners();
    }

    private void UpdateEquipSpawner(InventoryEquip equip)
    {
        InventoryStuff inventoryItem = equip;
        int index = GetSpawnerIndex(equip);

        if (index != -1 && spawners[index].IsInit) spawners[index].SpawnInventoryItemUI(inventoryItem);
    }

    private void DespawnEquip(InventoryEquip equip)
    {
        InventoryStuff inventoryItem = equip;
        int index = GetSpawnerIndex(equip);

        if (index != -1) spawners[index].DespawnInventoryItemUI(inventoryItem);
    }

    private int GetSpawnerIndex(InventoryEquip equip)
    {
        return equip.EquipSO.EquipType switch
        {
            EquipType.Weapon => 1,
            EquipType.Helmet => 2,
            EquipType.Armor => 3,
            EquipType.Armwear => 4,
            EquipType.Boots => 5,
            EquipType.Special => 6,
            _ => -1
        };
    }

    private void AddClickListeners()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;

            buttons[i].onClick.AddListener(() => Click(index));
        }
    }

    private void Click(int index)
    {
        if (currentOption == index) return;

        options[currentOption].sizeDelta = new Vector2(35f, 35f);

        spawners[currentOption].gameObject.SetActive(false);

        options[index].sizeDelta = new Vector2(50f, 50f);

        spawners[index].gameObject.SetActive(true);

        currentOption = index;
    }

    private void UpdateNewLockItem(InventoryEquip equip, bool inv)
    {
        if (!inv) return;

        int index = GetSpawnerIndex(equip);
        
        spawners[index].GetInventoryItemUIAndChangeNewLock(equip);
    }
}