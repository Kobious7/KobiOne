using System;
using UnityEngine;

public class MapLevel : MapAb
{
    [SerializeField] private int currentLevel = 0;

    public int CurrentLevel => currentLevel;

    [SerializeField] private int previousLevel = 0;

    public int PreviousLevel => previousLevel;

    private InfiniteMapManager infiniteMapManager;
    
    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;

        currentLevel = infiniteMapManager.MapData.MapCanLoad ? infiniteMapManager.MapData.MapInfo.Level : infiniteMapManager.PlayerData.MapLevel;
        previousLevel = currentLevel;
    }

    private void FixedUpdate()
    {
        if (!Changed()) return;

        previousLevel = currentLevel;

        //Save Data
        infiniteMapManager.PlayerData.Distance = previousLevel * 500;
        infiniteMapManager.PlayerData.MapLevel = previousLevel;
    }

    private bool Changed()
    {
        currentLevel = (int)Map.Distance / 500;

        if (currentLevel == previousLevel) return false;

        return true;
    }
}