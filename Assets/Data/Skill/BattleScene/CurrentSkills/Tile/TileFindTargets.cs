using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TileFindTargets : GMono
    {
        [SerializeField] private List<Transform> tileTargets;

        public List<Transform> TileTargets => tileTargets;

        [SerializeField] private List<O> affectArea;

        public List<O> AffectArea => affectArea;

        private TileSkillSO skillProps;

        public TileSkillSO SkillProps
        {
            get => skillProps;
            set => skillProps = value;
        }

        public void GetTileTargets(int number, string areaString)
        {
            tileTargets = new();
            affectArea = new();
            Game.Instance.TileSpawner.GetGeneratedTilesList();

            if(areaString == "rows")
            {
                GetRowsTargets(number);
            }
            else
            {
                GetNormalTargets(number);
            }
        }

        private void GetRowsTargets(int number)
        {
            List<int> rowIndex = new List<int>{0, 1, 2, 3, 4, 5, 6, 7};

            int rowNum = 0;

            while(rowNum != number)
            {
                int rand = rowIndex[Random.Range(0, rowIndex.Count)];
                Tiles firstColumTile = Game.Instance.TileSpawner.GetFirstColumTile(rand);
                int index = rowIndex.IndexOf(rand);

                rowIndex.RemoveAt(index);

                List<O> areas = new List<O>{new O(0, 0), new O(1, 0), new O(2, 0), new O(3, 0), new O(4, 0), new O(5, 0), new O(6, 0), new O(7, 0)};

                Tiles trueTarget = Game.Instance.TileSpawner.GetTileByXY(firstColumTile.TilePrefab.X + 7, firstColumTile.TilePrefab.Y);
                tileTargets.Add(trueTarget.transform);
                
                foreach(var area in areas)
                {
                    Tiles tileXY = Game.Instance.TileSpawner.GetTileByXY(area.X + firstColumTile.TilePrefab.X, area.Y + firstColumTile.TilePrefab.Y);
                    affectArea.Add(new O(tileXY.TilePrefab.X, tileXY.TilePrefab.Y));
                }

                rowNum++;
            }
        }

        private void GetNormalTargets(int number)
        {
            while(tileTargets.Count != number)
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
}
