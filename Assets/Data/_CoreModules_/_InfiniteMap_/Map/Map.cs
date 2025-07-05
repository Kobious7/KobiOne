using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : GMono
{
    private InfiniteMapSO mapData;

    [SerializeField] private List<Transform> maps;

    public List<Transform> Maps => maps;

    [SerializeField] private float distance = 0;

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

    [SerializeField] private MapLevel mapLevel;

    public MapLevel MapLevel => mapLevel;

    [SerializeField] private MapSwap mapSwap;

    public MapSwap MapSwap => mapSwap;

    [SerializeField] private MapReset mapReset;

    public MapReset MapReset => mapReset;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadData();
        LoadMaps();
        LoadMapLevel();
        LoadMapSwap();
        LoadMapReset();
    }

    protected override void Start()
    {
        base.Start();
        if(InfiniteMapManager.Instance.MapData.MapCanLoad)
        {
            distance = InfiniteMapManager.Instance.MapData.MapInfo.Distance;
        }
    }

    protected void LoadData()
    {
        if (mapData != null) return;

        mapData = Resources.Load<InfiniteMapSO>("InfiniteMapData");
    }

    private void LoadMaps()
    {
        if (maps.Count > 1) return;

        List<Tilemap> list = GetComponentsInChildren<Tilemap>().ToList();

        foreach (Tilemap tilemap in list)
        {
            maps.Add(tilemap.transform);
        }
    }

    private void LoadMapLevel()
    {
        if (mapLevel != null) return;

        mapLevel = GetComponentInChildren<MapLevel>();
    }

    private void LoadMapSwap()
    {
        if (mapSwap != null) return;

        mapSwap = GetComponentInChildren<MapSwap>();
    }

    private void LoadMapReset()
    {
        if (mapReset != null) return;

        mapReset = GetComponentInChildren<MapReset>();
    }
}