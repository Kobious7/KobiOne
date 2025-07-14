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

    [SerializeField] private List<Transform> standbyMaps;

    public List<Transform> StandbyMaps => standbyMaps;

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

        if (standbyMaps.Count <= 0)
        {
            standbyMaps.Add(transform.Find("Map1").Find("StandbyMap"));
            standbyMaps.Add(transform.Find("Map2").Find("StandbyMap"));
        }
    }

    protected override void Start()
    {
        base.Start();
        if (InfiniteMapManager.Instance.MapData.MapCanLoad)
        {
            distance = InfiniteMapManager.Instance.MapData.MapInfo.Distance;
        }

        if (distance <= 500)
        {
            standbyMaps[0].gameObject.SetActive(true);
            standbyMaps[1].gameObject.SetActive(false);
        }
        else
        {
            standbyMaps[0].gameObject.SetActive(false);
            standbyMaps[1].gameObject.SetActive(false);
        }

        mapSwap.OnMapSwap += CheckStandbyMap;
    }

    protected void LoadData()
    {
        if (mapData != null) return;

        mapData = Resources.Load<InfiniteMapSO>("InfiniteMapData");
    }

    private void LoadMaps()
    {
        Debug.Log(maps.Count);
        if (maps.Count > 0) return;

        maps.Add(transform.Find("Map1"));
        maps.Add(transform.Find("Map2"));
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

    private void CheckStandbyMap(MapEnum currentMap)
    {
        if (distance <= 500)
        {
            if (currentMap == MapEnum.Map0)
            {
                standbyMaps[0].gameObject.SetActive(true);
            }
            else
            {
                standbyMaps[1].gameObject.SetActive(true);
            }
        }
        else
        {
            standbyMaps[0].gameObject.SetActive(false);
            standbyMaps[1].gameObject.SetActive(false);
        }
    }
}