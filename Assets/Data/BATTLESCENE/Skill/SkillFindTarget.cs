using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SkillFindTarget : SkillAb
    {
        [SerializeField] private List<Transform> targets;

        public List<Transform> Targets => targets;

        public void GetTargets()
        {
            targets = new();

            while (targets.Count != Skill.Properties.SpawnCount)
            {
                Tiles target = Game.Instance.TileSpawner.GetRandomTile();

                while (!IsOKTarget(target))
                {
                    target = Game.Instance.TileSpawner.GetRandomTile();
                }

                Debug.Log($"X: {target.TilePrefab.X}, Y: {target.TilePrefab.Y}");

                if (!targets.Contains(target.transform))
                {
                    MakeMarkList(Game.Instance.TileSpawner.MarkList, target);
                    targets.Add(target.transform);
                }
            }
        }

        private bool IsOKTarget(Tiles tile)
        {
            int[,] range = Skill.Properties.Range;

            for (int i = 0; i < range.Length / 2; i++)
            {
                if (tile.TilePrefab.X + range[i, 0] >= 8 || tile.TilePrefab.X + range[i, 0] < 0
                    || tile.TilePrefab.Y + range[i, 1] >= 8 || tile.TilePrefab.Y + range[i, 1] < 0)
                    return false;
            }

            return true;
        }

        private void MakeMarkList(List<int[,]> markList, Tiles tile)
        {
            int[,] range = Skill.Properties.Range;

            for (int i = 0; i < range.Length / 2; i++)
            {
                int x = tile.TilePrefab.X + range[i, 0];
                int y = tile.TilePrefab.Y + range[i, 1];
                int[,] xy = { { x, y } };

                markList.Add(xy);
            }
        }
    }
}