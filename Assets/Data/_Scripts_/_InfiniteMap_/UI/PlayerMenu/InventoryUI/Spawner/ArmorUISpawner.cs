using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorUISpawner : Spawner
{
    private static ArmorUISpawner instance;

    public static ArmorUISpawner Instance => instance;

    [SerializeField] private List<InventoryEquip> armorList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 ArmorUISpawner is allowed to exist!");

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

        armorList = InfiniteMapManager.Instance.Inventory.ArmorList;
        SpawnArmorUI();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnArmorUI()
    {
        for(int i = 0; i < armorList.Count; i++)
        {
            Transform newArmor = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newArmor.transform.localScale = Vector3.one;
            ArmorUI bodyArmor = newArmor.GetComponent<ArmorUI>();
            bodyArmor.Index = i;
            bodyArmor.Equip = armorList[i];

            bodyArmor.ShowArmor(armorList[i]);
            newArmor.gameObject.SetActive(true);
        }
    }
}