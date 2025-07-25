using UnityEngine;

public class BattleQuitGame : QuitGame
{
    protected override void GetMapData()
    {
        base.GetMapData();

        mapData = BattleManager.Instance.MapData;
    }
}