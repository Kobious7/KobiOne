using UnityEngine;

public class InfinimapQuitGame : QuitGame
{
    protected override void GetMapData()
    {
        base.GetMapData();

        mapData = InfiniteMapManager.Instance.MapData;
    }
}