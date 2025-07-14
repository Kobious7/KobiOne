using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmwearUISpawner : Spawner
{
    private static ArmwearUISpawner instance;

    public static ArmwearUISpawner Instance => instance;

    [SerializeField] private List<InventoryEquip> armwearList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 ArmwearUISpawner is allowed to exist!");

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

        armwearList = InfiniteMapManager.Instance.Inventory.ArmwearList;
        SpawnArmwearUI();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnArmwearUI()
    {
        for(int i = 0; i < armwearList.Count; i++)
        {
            Transform newArmwear = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newArmwear.transform.localScale = Vector3.one;
            ArmwearUI armwearUI = newArmwear.GetComponent<ArmwearUI>();
            armwearUI.Index = i;
            armwearUI.Equip = armwearList[i];

            armwearUI.ShowArmwear(armwearList[i]);
            newArmwear.gameObject.SetActive(true);
        }
    }
}