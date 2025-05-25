using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmetUISpawner : Spawner
{
    private static HelmetUISpawner instance;

    public static HelmetUISpawner Instance => instance;

    [SerializeField] private List<InventoryEquip> helmetList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 HelmetUISpawner is allowed to exist!");

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

        helmetList = Game.Instance.Inventory.HelmetList;
        SpawnHelmetUI();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnHelmetUI()
    {
        for(int i = 0; i < helmetList.Count; i++)
        {
            Transform newHelmet = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newHelmet.transform.localScale = Vector3.one;
            HelmetUI helmetUI = newHelmet.GetComponent<HelmetUI>();
            helmetUI.Index = i;
            helmetUI.Equip = helmetList[i];

            helmetUI.ShowHelmet(helmetList[i]);
            newHelmet.gameObject.SetActive(true);
        }
    }
}