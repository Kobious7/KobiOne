using System;
using UnityEngine;

public class MapLevel : MapAb
{
    [SerializeField] private int currentLevel = 0;

    public int CurrentLevel => currentLevel;

    [SerializeField] private int previousLevel = 0;

    public int PreviousLevel => previousLevel;
    
    protected override void Start()
    {
        base.Start();
        
        if(!InfiniteMapManager.Instance.MapData.MapCanLoad) return;

        currentLevel = InfiniteMapManager.Instance.MapData.MapInfo.Level;
        previousLevel = currentLevel;
    }

    private void FixedUpdate()
    {
        if (!Changed()) return;

        previousLevel = currentLevel;
    }

    private bool Changed()
    {
        currentLevel = (int)Map.Distance / 500;

        if (currentLevel == previousLevel) return false;

        return true;
    }
}