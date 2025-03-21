using InfiniteMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Game.Instance.MapData.MapCanLoad = false;
        Game.Instance.MapData.Result = Result.NONE;
        Game.Instance.MapData.InfiniteMapInfo = new();
        Game.Instance.MapData.PlayerInfo = new();
        Game.Instance.MapData.MonsterInfo = new();
        Game.Instance.MapData.ItemDropList = new();
        Game.Instance.MapData.EquipDropList = new();
        Game.Instance.MapData.Map1MonsterSpawnerInfo = new();
        Game.Instance.MapData.Map2MonsterSpawnerInfo= new();
        Game.Instance.MapData.ListItems = new();
        Game.Instance.MapData.WeaponList = new();
        Game.Instance.MapData.HelmetList = new();
        Game.Instance.MapData.BodyArmorList = new();
        Game.Instance.MapData.LegArmorList = new();
        Game.Instance.MapData.BootsList = new();
        Game.Instance.MapData.AuraList = new();
        Game.Instance.MapData.BackItemList = new();
        //SavingManager.Instance.SavePlayerData();
    }
}
