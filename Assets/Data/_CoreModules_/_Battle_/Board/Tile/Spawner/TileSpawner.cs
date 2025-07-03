using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawner : PrefabSpawner
{
    [SerializeField] protected List<TileBoard> generatedTilesList;

    public List<TileBoard> GeneratedTilesList => generatedTilesList;

    [SerializeField] private List<int[,]> markList;

    public List<int[,]> MarkList
    {
        get => markList;
        set => markList = value;
    }

    public Transform SpawnTilePrefab(TileEnum tileType, Vector3 pos, Quaternion rot)
    {
        Transform tile = Spawn(GetPrefabByTileType(tileType), pos, rot);
        tile.name = "Tile";

        return tile;
    }

    public void GetGeneratedTilesList()
    {
        TileBoard[] list = holder.GetComponentsInChildren<TileBoard>();

        foreach (TileBoard tile in list)
            if (tile.TileProperties.X < 8 && tile.TileProperties.Y < 8) generatedTilesList.Add(tile);
    }

    public TileBoard GetRandomTile()
    {
        return generatedTilesList[Random.Range(0, generatedTilesList.Count)];
    }

    public TileBoard Get00()
    {
        return generatedTilesList[0];
    }

    public TileBoard GetFirstColumTile(int y)
    {
        foreach (TileBoard tile in generatedTilesList)
        {
            if (tile.TileProperties.X == 0 && tile.TileProperties.Y == y) return tile;
        }

        return null;
    }

    public TileBoard GetTileByXY(int x, int y)
    {
        foreach (TileBoard tile in generatedTilesList)
        {
            if (tile.TileProperties.X == x && tile.TileProperties.Y == y) return tile;
        }

        return null;
    }

    public void DespawnAllTiles()
    {
        List<TileBoard> objs = holder.GetComponentsInChildren<TileBoard>().ToList();

        foreach (TileBoard obj in objs)
        {
            if (objPool.Contains(obj.transform)) continue;
            objPool.Add(obj.transform);
            obj.gameObject.SetActive(false);
        }
    }

    public Transform GetPrefabByTileType(TileEnum tileType)
    {
        foreach (Transform prefab in prefabs)
        {
            TileBoard tile = GetTile(prefab);

            if (tile != null && tile.TileProperties.TileEnum == tileType)
            {
                return prefab;
            }

            continue;
        }

        return null;
    }
}