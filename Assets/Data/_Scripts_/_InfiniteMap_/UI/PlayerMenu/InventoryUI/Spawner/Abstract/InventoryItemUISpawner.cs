using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryItemUISpawner : Spawner
{
    [SerializeField] private List<InventoryStuff> inventoryItemList;
    [SerializeField] private List<InventoryItemUI> inventoryItemUIList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        inventoryItemList = GetInventoryItemList();
        inventoryItemUIList = new();

        SpawnInventoryItemUIs();
    }

    protected abstract List<InventoryStuff> GetInventoryItemList();

    protected override void LoadHolder()
    {
        if (holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnInventoryItemUIs()
    {
        foreach (var inventoryItem in inventoryItemList)
        {
            SpawnInventoryItemUI(inventoryItem);
        }
    }

    public void SpawnInventoryItemUI(InventoryStuff inventoryItem)
    {
        Transform newInventoryItem = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
        newInventoryItem.transform.localScale = Vector3.one;
        InventoryItemUI inventoryItemUICom = newInventoryItem.GetComponent<InventoryItemUI>();
        inventoryItemUICom.InventoryItem = inventoryItem;
        inventoryItemUIList.Add(inventoryItemUICom);

        inventoryItemUICom.ShowIventoryItem(inventoryItemUICom);

        newInventoryItem.gameObject.SetActive(true);
    }

    public void DespawnInventoryItemUI(InventoryStuff inventoryItem)
    {
        Transform inventoryItemUI = inventoryItemUIList.Where(e => e.InventoryItem == inventoryItem).FirstOrDefault().transform;

        inventoryItemUIList.Remove(inventoryItemUI.GetComponent<InventoryItemUI>());
        Despawn(inventoryItemUI);
    }

    public void GetInventoryItemUIAndChangeNewLock(InventoryEquip equip)
    {
        InventoryItemUI inventoryItemUI = inventoryItemUIList.Where(e => e.InventoryItem == equip).FirstOrDefault();
        InvEquipUI equipUI = (InvEquipUI)inventoryItemUI;
        
        equipUI.ReshowNewLock();
    }
}