using System;
using UnityEngine;

public class MapReset : MapAb
{
    public event Action OnMapReset;
    [SerializeField] private bool canReset;
    [SerializeField] private int xMax;

    private void FixedUpdate()
    {
        ResetWorld();
        if (!canReset) return;

        OnMapReset?.Invoke();

        canReset = false;
    }

    private void ResetWorld()
    {
        if (Map.Maps[0].position.x == xMax || Map.Maps[1].position.x == xMax) canReset = true;
    }
}
