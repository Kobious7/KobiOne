using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackItemUISpawner : Spawner
{
    private static BackItemUISpawner instance;

    public static BackItemUISpawner Instance => instance;

    [SerializeField] private List<InventoryEquip> backItemList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 BackItemUISpawner is allowed to exist!");

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

        backItemList = InfiniteMapManager.Instance.Inventory.BackItemList;
        SpawnBackItemUI();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnBackItemUI()
    {
        for(int i = 0; i < backItemList.Count; i++)
        {
            Transform newBackItem = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newBackItem.transform.localScale = Vector3.one;
            BackItemUI backItemUI = newBackItem.GetComponent<BackItemUI>();
            backItemUI.Index = i;
            backItemUI.Equip = backItemList[i];

            backItemUI.ShowBackItem(backItemList[i]);
            newBackItem.gameObject.SetActive(true);
        }
    }
}