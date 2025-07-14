using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        InfiniteMapManager.Instance.MapData.MapCanLoad = false;
        InfiniteMapManager.Instance.MapData.Result = Result.NONE;
        InfiniteMapManager.Instance.MapData.MapInfo = new();
        InfiniteMapManager.Instance.MapData.PlayerInfo = new();
        InfiniteMapManager.Instance.MapData.MonsterInfo = new();
        InfiniteMapManager.Instance.MapData.ItemDropList = new();
        InfiniteMapManager.Instance.MapData.EquipDropList = new();
        InfiniteMapManager.Instance.MapData.Map1MonsterSpawnerInfo = new();
        InfiniteMapManager.Instance.MapData.Map2MonsterSpawnerInfo = new();
        InfiniteMapManager.Instance.MapData.ItemList = new();
        InfiniteMapManager.Instance.MapData.WeaponList = new();
        InfiniteMapManager.Instance.MapData.HelmetList = new();
        InfiniteMapManager.Instance.MapData.ArmorList = new();
        InfiniteMapManager.Instance.MapData.ArmwearList = new();
        InfiniteMapManager.Instance.MapData.BootsList = new();
        InfiniteMapManager.Instance.MapData.SpecialList = new();
        //SavingManager.Instance.SavePlayerData();
    }
}
