using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailShopUISpawner : Spawner
{
    [SerializeField] private TrailShopSO trailShop;

    protected override void LoadHolder()
    {
        holder = GetComponent<ScrollRect>().content;
        trailShop = Resources.Load<TrailShopSO>("TrailShop/TrailShop");
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        InitShop();
    }

    private void InitShop()
    {
        int index = 0;

        foreach (var item in trailShop.ItemList)
        {
            if (index == 0) SpawnItem(prefabs[0], item);
            else if (index == 1) SpawnItem(prefabs[1], item);
            else SpawnItem(prefabs[2], item);

            index++;
        }
    }

    private void SpawnItem(Transform prefab, TrailShopItem item)
    {
        Transform itemTrailShopUI = Spawn(prefab, Vector3.zero, Quaternion.identity);
        itemTrailShopUI.localScale = Vector3.one;

        itemTrailShopUI.gameObject.SetActive(true);

        TrailShopItemUI trailShopItemUICom = itemTrailShopUI.GetComponent<TrailShopItemUI>();
        trailShopItemUICom.SetTrailShopItemUI(item);
    }
}