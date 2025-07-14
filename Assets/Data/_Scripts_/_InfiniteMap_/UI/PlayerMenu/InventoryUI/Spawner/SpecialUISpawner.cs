using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialUISpawner : Spawner
{
    private static SpecialUISpawner instance;

    public static SpecialUISpawner Instance => instance;

    [SerializeField] private List<InventoryEquip> specialList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 SpecialUISpawner is allowed to exist!");

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

        specialList = InfiniteMapManager.Instance.Inventory.SpecialList;
        SpawnSpecialUI();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnSpecialUI()
    {
        for(int i = 0; i < specialList.Count; i++)
        {
            Transform newSpecial = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newSpecial.transform.localScale = Vector3.one;
            SpecialUI specialUI = newSpecial.GetComponent<SpecialUI>();
            specialUI.Index = i;
            specialUI.Equip = specialList[i];

            specialUI.ShowSpecial(specialList[i]);
            newSpecial.gameObject.SetActive(true);
        }
    }
}