using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ItemUISpawner : Spawner
{
    private static ItemUISpawner instance;

    public static ItemUISpawner Instance => instance;

    [SerializeField] private List<InventoryItem> spawnList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 ItemUISpawner is allowed to exist!");

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

        spawnList = InfiniteMapManager.Instance.Inventory.ItemList;
        SpawnItemUI();
    }

    protected override void LoadHolder()
    {
        if (holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnItemUI()
    {
        for(int i = 0; i < spawnList.Count; i++)
        {
            Transform newItem = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newItem.transform.localScale = Vector3.one;
            ItemUI itemUI = newItem.GetComponent<ItemUI>();
            itemUI.Index = i;
            itemUI.ItemSO = spawnList[i].ItemSO;

            itemUI.ShowItem(spawnList[i]);
            newItem.gameObject.SetActive(true);
        }
    }
}