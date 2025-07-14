using System;
using System.Collections;
using UnityEngine;

public class MapSwap : MapAb
{
    public event Action<MapEnum> OnMapSwap;
    public event Action<MapEnum> OnMapChange;
    [SerializeField] private Transform player;
    [SerializeField] private MapEnum currentMap;

    public MapEnum CurrentMap => currentMap;

    [SerializeField] private float dis1, dis2, swapDistance = 30;
    [SerializeField] private bool map0, map1;
    [SerializeField] private bool canSwap;
    [SerializeField] private MapEnum previousMap = MapEnum.Map0;

    public bool CanSwap
    {
        get => canSwap;
        set => canSwap = value;
    }

    protected override void Start()
    {
        base.Start();
        player = InfiniteMapManager.Instance.Player.transform;
        currentMap = MapEnum.Map0;
        map0 = true;
        canSwap = true;
        OnMapSwap?.Invoke(currentMap);
    }

    private void FixedUpdate()
    {
        if (previousMap != currentMap)
        {
            previousMap = currentMap;
            OnMapChange?.Invoke(currentMap);
        }

        if (Map.Distance <= 0) return;

        RightSwapMap();
        LeftSwapMap();

        if (!CheckCurrentMap()) return;

        map0 = !map0;
        map1 = !map1;

        if (map0) currentMap = MapEnum.Map0;
        if (map1) currentMap = MapEnum.Map1;

        canSwap = true;

        OnMapSwap?.Invoke(currentMap);
    }

    private bool CheckCurrentMap()
    {
        dis1 = Mathf.Abs(player.position.x - Map.Maps[0].position.x);
        dis2 = Mathf.Abs(player.position.x - Map.Maps[1].position.x);

        if (map0 != dis1 < dis2) return true;
        if (map1 != dis2 < dis1) return true;

        return false;
    }

    private void RightSwapMap()
    {
        if (InputManager.Instance.Horizontal <= 0) return;
        if (!canSwap) return;

        if (currentMap == MapEnum.Map0 && Mathf.Abs(player.position.x - (Map.Maps[0].position.x + 250)) < swapDistance && Map.Maps[1].position.x < Map.Maps[0].position.x)
        {
            Map.Maps[1].position = new Vector3(Map.Maps[1].position.x + 1000, Map.Maps[1].position.y, 0);
            canSwap = false;
        }

        if (currentMap == MapEnum.Map1 && Mathf.Abs(player.position.x - (Map.Maps[1].position.x + 250)) < swapDistance && Map.Maps[0].position.x < Map.Maps[1].position.x)
        {
            Map.Maps[0].position = new Vector3(Map.Maps[0].position.x + 1000, Map.Maps[0].position.y, 0);
            canSwap = false;
        }
    }

    private void LeftSwapMap()
    {
        if (InputManager.Instance.Horizontal >= 0) return;
        if (!canSwap) return;

        if (currentMap == MapEnum.Map0 && Mathf.Abs(player.position.x - (Map.Maps[0].position.x - 250)) < swapDistance && Map.Maps[0].position.x < Map.Maps[1].position.x)
        {
            Map.Maps[1].position = new Vector3(Map.Maps[1].position.x - 1000, Map.Maps[1].position.y, 0);
            canSwap = false;
        }

        if (currentMap == MapEnum.Map1 && Mathf.Abs(player.position.x - (Map.Maps[1].position.x - 250)) < swapDistance && Map.Maps[1].position.x < Map.Maps[0].position.x)
        {
            Map.Maps[0].position = new Vector3(Map.Maps[0].position.x - 1000, Map.Maps[0].position.y, 0);
            canSwap = false;
        }
    }
}
