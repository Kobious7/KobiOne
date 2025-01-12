using System.Collections.Generic;
using Battle;
using UnityEngine;

public class QTileFindTargets : GMono
{
    [SerializeField] private List<Transform> tileTargets;

    public List<Transform> TileTargets => tileTargets;

    [SerializeField] private List<O> affectArea;

    public List<O> AffectArea => affectArea;

    private TileSkillSO skillProps;

    protected override void Start()
    {
        base.Start();
        skillProps = (TileSkillSO)Skills.Instance.QSkill;
    }

    public void GetTileTargets()
    {
        tileTargets = new();
        affectArea = new();
        Game.Instance.TileSpawner.GetGeneratedTilesList();

        while(tileTargets.Count != skillProps.ObjectSpawnCount)
        {
            Tiles rand = Game.Instance.TileSpawner.GetRandomTile();

            while(!IsOKTarget(rand))
            {
                rand = Game.Instance.TileSpawner.GetRandomTile();
            }

            if(!tileTargets.Contains(rand.transform))
            {
                tileTargets.Add(rand.transform);
                CreateAffectArea(rand);
            }
        }
    }

    private bool IsOKTarget(Tiles tile)
    {
        List<O> area = skillProps.Area;

        for (int i = 0; i < area.Count; i++)
        {
            if (tile.TilePrefab.X + area[i].X >= 8 || tile.TilePrefab.X + area[i].X < 0
                || tile.TilePrefab.Y + area[i].Y >= 8 || tile.TilePrefab.Y + area[i].Y < 0)
                return false;
        }

        return true;
    }

    private void CreateAffectArea(Tiles tile)
    {
        int x = tile.TilePrefab.X;
        int y = tile.TilePrefab.Y;

        foreach(O o in skillProps.Area)
        {
            affectArea.Add(new O(x + o.X, y + o.Y));
        }
    }
}