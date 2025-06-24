using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUISpawner : Spawner
{
    private static WeaponUISpawner instance;

    public static WeaponUISpawner Instance => instance;

    [SerializeField] private List<InventoryEquip> weaponList;
    [SerializeField] private Transform selectedItem;

    public Transform SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 WeaponUISpawner is allowed to exist!");

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

        weaponList = InfiniteMapManager.Instance.Inventory.WeaponList;
        SpawnWeaponUI();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnWeaponUI()
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            Transform newWeapon = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newWeapon.transform.localScale = Vector3.one;
            WeaponUI weaponUI = newWeapon.GetComponent<WeaponUI>();
            weaponUI.Index = i;
            weaponUI.Equip = weaponList[i];

            weaponUI.ShowWeapon(weaponList[i]);
            newWeapon.gameObject.SetActive(true);
        }
    }
}