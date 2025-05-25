using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawner : Spawner
{
    [SerializeField] private Transform tileObject;

    public Transform TileObject => tileObject;

    [SerializeField] protected List<Tiles> generatedTilesList;

    public List<Tiles> GeneratedTilesList => generatedTilesList;

    [SerializeField] private List<int[,]> markList;

    public List<int[,]> MarkList
    {
        get => markList;
        set => markList = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileObject();
    }

    private void LoadTileObject()
    {
        if (tileObject != null) return;

        tileObject = transform.Find("Prefabs").Find("Tile");
    }

    public void GetGeneratedTilesList()
    {
        Tiles[] list = holder.GetComponentsInChildren<Tiles>();

        foreach (Tiles tile in list)
            if (tile.TilePrefab.X < 8 && tile.TilePrefab.Y < 8) generatedTilesList.Add(tile);
    }

    public Tiles GetRandomTile()
    {
        return generatedTilesList[Random.Range(0, generatedTilesList.Count)];
    }

    public Tiles Get00()
    {
        return generatedTilesList[0];
    }

    public Tiles GetFirstColumTile(int y)
    {
        foreach(Tiles tile in generatedTilesList)
        {
            if(tile.TilePrefab.X == 0 && tile.TilePrefab.Y == y) return tile;
        }

        return null;
    }

    public Tiles GetTileByXY(int x, int y)
    {
        foreach(Tiles tile in generatedTilesList)
        {
            if(tile.TilePrefab.X == x && tile.TilePrefab.Y == y) return tile;
        }

        return null;
    }

    public void DespawnAllTiles()
    {
        List<Tiles> objs = holder.GetComponentsInChildren<Tiles>().ToList();

        foreach (Tiles obj in objs)
        {
            if (objPool.Contains(obj.transform)) continue;
            objPool.Add(obj.transform);
            obj.gameObject.SetActive(false);
        }
    }
}