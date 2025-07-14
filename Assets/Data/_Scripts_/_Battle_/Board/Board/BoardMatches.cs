using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardMatches : BoardAb
{
    [SerializeField] private List<TileCanBeMatches> matches;

    public List<TileCanBeMatches> Matches => matches;

    private Transform[,] tiles;
    private int count = 0;

    public void SkillMarkMatches(Transform[,] tiles)
    {
        List<int[,]> markList = BattleManager.Instance.TileSpawner.MarkList;

        foreach (var item in markList)
        {
            TileBoard tile = GetTile(tiles[item[0, 0], item[0, 1]]);

            if (tile.TileProperties.CanBeDestroyed == false)
                tile.TileProperties.CanBeDestroyed = true;
        }
    }

    public void SkillsMarkMatches(TileSkill tileSkill, Transform[,] tiles)
    {
        foreach (var item in tileSkill.TargetsFinder.AffectArea)
        {
            TileBoard tile = GetTile(tiles[item.X, item.Y]);

            if (tile.TileProperties.CanBeDestroyed == false)
                tile.TileProperties.CanBeDestroyed = true;
        }
    }

    public void MarkAsMatches(Transform[,] tiles)
    {
        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                if (tiles[x, y] == null) continue;

                TileBoard tile = GetTile(tiles[x, y]);

                if (tile.TileProperties.CanBeDestroyed == false)
                {
                    if (y + 2 < Board.Size)
                    {
                        TileBoard tileXY1 = GetTile(tiles[x, y + 1]);
                        TileBoard tileXY2 = GetTile(tiles[x, y + 2]);

                        if (tile.TileProperties.TileEnum == tileXY1.TileProperties.TileEnum
                            && tile.TileProperties.TileEnum == tileXY2.TileProperties.TileEnum)
                        {
                            tile.TileProperties.CanBeDestroyed = true;
                            if (tileXY1.TileProperties.CanBeDestroyed == false) tileXY1.TileProperties.CanBeDestroyed = true;
                            if (tileXY2.TileProperties.CanBeDestroyed == false) tileXY2.TileProperties.CanBeDestroyed = true;
                        }
                    }

                    if (x + 2 < Board.Size)
                    {
                        TileBoard tileX1Y = GetTile(tiles[x + 1, y]);
                        TileBoard tileX2Y = GetTile(tiles[x + 2, y]);

                        if (tile.TileProperties.TileEnum == tileX1Y.TileProperties.TileEnum
                            && tile.TileProperties.TileEnum == tileX2Y.TileProperties.TileEnum)
                        {
                            tile.TileProperties.CanBeDestroyed = true;
                            if (tileX1Y.TileProperties.CanBeDestroyed == false) tileX1Y.TileProperties.CanBeDestroyed = true;
                            if (tileX2Y.TileProperties.CanBeDestroyed == false) tileX2Y.TileProperties.CanBeDestroyed = true;
                        }
                    }

                    if (y - 2 >= 0)
                    {
                        TileBoard tileXYM1 = GetTile(tiles[x, y - 1]);
                        TileBoard tileXYM2 = GetTile(tiles[x, y - 2]);

                        if (tile.TileProperties.TileEnum == tileXYM1.TileProperties.TileEnum
                            && tile.TileProperties.TileEnum == tileXYM2.TileProperties.TileEnum)
                        {
                            tile.TileProperties.CanBeDestroyed = true;
                            if (tileXYM1.TileProperties.CanBeDestroyed == false) tileXYM1.TileProperties.CanBeDestroyed = true;
                            if (tileXYM2.TileProperties.CanBeDestroyed == false) tileXYM2.TileProperties.CanBeDestroyed = true;
                        }
                    }

                    if (x - 2 >= 0)
                    {
                        TileBoard tileXM1Y = GetTile(tiles[x - 1, y]);
                        TileBoard tileXM2Y = GetTile(tiles[x - 2, y]);

                        if (tile.TileProperties.TileEnum == tileXM1Y.TileProperties.TileEnum
                            && tile.TileProperties.TileEnum == tileXM2Y.TileProperties.TileEnum)
                        {
                            tile.TileProperties.CanBeDestroyed = true;
                            if (tileXM1Y.TileProperties.CanBeDestroyed == false) tileXM1Y.TileProperties.CanBeDestroyed = true;
                            if (tileXM2Y.TileProperties.CanBeDestroyed == false) tileXM2Y.TileProperties.CanBeDestroyed = true;
                        }
                    }
                }
            }
        }
    }

    public bool CanBeDestroyed(TileBoard tile, int x, int y, Transform[,] tiles, TileDirection direction)
    {
        if (y + 1 < Board.Size && direction != TileDirection.BOTTOM)
        {
            TileBoard tileXY1 = GetTile(tiles[x, y + 1]);

            if (y - 1 >= 0)
            {
                TileBoard tileXYM1 = GetTile(tiles[x, y - 1]);

                if (tile.TileProperties.TileEnum == tileXY1.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileXYM1.TileProperties.TileEnum && direction != TileDirection.TOP)
                    return true;
            }

            if (y + 2 < Board.Size)
            {
                TileBoard tileXY2 = GetTile(tiles[x, y + 2]);

                if (tile.TileProperties.TileEnum == tileXY1.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileXY2.TileProperties.TileEnum)
                    return true;
            }
        }

        if (x + 1 < Board.Size && direction != TileDirection.LEFT)
        {
            TileBoard tileX1Y = GetTile(tiles[x + 1, y]);

            if (x - 1 >= 0)
            {
                TileBoard tileXM1Y = GetTile(tiles[x - 1, y]);

                if (tile.TileProperties.TileEnum == tileX1Y.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileXM1Y.TileProperties.TileEnum && direction != TileDirection.RIGHT)
                    return true;
            }

            if (x + 2 < Board.Size)
            {
                TileBoard tileX2Y = GetTile(tiles[x + 2, y]);

                if (tile.TileProperties.TileEnum == tileX1Y.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileX2Y.TileProperties.TileEnum)
                    return true;
            }
        }

        if (y - 1 >= 0 && direction != TileDirection.TOP)
        {
            TileBoard tileXYM1 = GetTile(tiles[x, y - 1]);

            if (y + 1 < Board.Size)
            {
                TileBoard tileXY1 = GetTile(tiles[x, y + 1]);

                if (tile.TileProperties.TileEnum == tileXYM1.TileProperties.TileEnum
                        && tile.TileProperties.TileEnum == tileXY1.TileProperties.TileEnum && direction != TileDirection.BOTTOM)
                    return true;
            }

            if (y - 2 >= 0)
            {
                TileBoard tileXYM2 = GetTile(tiles[x, y - 2]);

                if (tile.TileProperties.TileEnum == tileXYM1.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileXYM2.TileProperties.TileEnum)
                    return true;
            }
        }

        if (x - 2 >= 0 && direction != TileDirection.RIGHT)
        {
            TileBoard tileXM1Y = GetTile(tiles[x - 1, y]);

            if (x + 1 < Board.Size)
            {
                TileBoard tileX1Y = GetTile(tiles[x + 1, y]);

                if (tile.TileProperties.TileEnum == tileXM1Y.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileX1Y.TileProperties.TileEnum && direction != TileDirection.LEFT)
                    return true;
            }

            if (x - 2 >= 0)
            {
                TileBoard tileXM2Y = GetTile(tiles[x - 2, y]);

                if (tile.TileProperties.TileEnum == tileXM1Y.TileProperties.TileEnum
                    && tile.TileProperties.TileEnum == tileXM2Y.TileProperties.TileEnum)
                    return true;
            }
        }

        return false;
    }

    public List<TileCanBeMatches> GetListMatches()
    {
        matches = new();
        tiles = Board.BoardGen.Tiles;

        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                GetTopMatches(x, y);
                GetRightMatches(x, y);
                GetBottomMatches(x, y);
                GetLeftMatches(x, y);
            }
        }

        return matches;
    }

    private void GetTopMatches(int x, int y)
    {
        if (y + 1 >= Board.Size) return;

        int count = 1;

        TileBoard tileXY = GetTile(tiles[x, y]);

        if (x + 1 < Board.Size && x - 1 >= 0)
        {
            TileBoard tileX1Y = GetTile(tiles[x + 1, y + 1]);
            TileBoard tileXM1Y = GetTile(tiles[x - 1, y + 1]);

            if (tileX1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileXM1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum)
            {
                count += 2;

                if (x + 2 < Board.Size)
                {
                    TileBoard tileX2Y = GetTile(tiles[x + 2, y + 1]);

                    if (tileX2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }

                if (x - 2 >= 0)
                {
                    TileBoard tileXM2Y = GetTile(tiles[x - 2, y + 1]);

                    if (tileXM2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }
            }
            else
            {
                if (x + 2 < Board.Size)
                {
                    TileBoard tileX2Y = GetTile(tiles[x + 2, y + 1]);

                    if (tileX1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileX2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }

                if (x - 2 >= 0)
                {
                    TileBoard tileXM2Y = GetTile(tiles[x - 2, y + 1]);

                    if (tileXM1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileXM2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }
            }
        }

        if (y + 3 < Board.Size)
        {
            TileBoard tileXY2 = GetTile(tiles[x, y + 2]);
            TileBoard tileXY3 = GetTile(tiles[x, y + 3]);

            if (tileXY2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileXY3.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
        }

        if (count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.TOP, tileXY.TileProperties.TileEnum, count);

            matches.Add(tile);
        }
    }

    private void GetRightMatches(int x, int y)
    {
        if (x + 1 >= Board.Size) return;

        int count = 1;

        TileBoard tileXY = GetTile(tiles[x, y]);

        if (y + 1 < Board.Size && y - 1 >= 0)
        {
            TileBoard tileXY1 = GetTile(tiles[x + 1, y + 1]);
            TileBoard tileXYM1 = GetTile(tiles[x + 1, y - 1]);

            if (tileXY1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileXYM1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum)
            {
                count += 2;

                if (y + 2 < Board.Size)
                {
                    TileBoard tileXY2 = GetTile(tiles[x + 1, y + 2]);

                    if (tileXY2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }

                if (y - 2 >= 0)
                {
                    TileBoard tileXYM2 = GetTile(tiles[x + 1, y - 2]);

                    if (tileXYM2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }
            }
            else
            {
                if (y + 2 < Board.Size)
                {
                    TileBoard tileXY2 = GetTile(tiles[x + 1, y + 2]);

                    if (tileXY1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileXY2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }

                if (y - 2 >= 0)
                {
                    TileBoard tileXYM2 = GetTile(tiles[x + 1, y - 2]);

                    if (tileXYM1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileXYM2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }
            }
        }

        if (x + 3 < Board.Size)
        {
            TileBoard tileX2Y = GetTile(tiles[x + 2, y]);
            TileBoard tileX3Y = GetTile(tiles[x + 3, y]);

            if (tileX2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileX3Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
        }

        if (count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.RIGHT, tileXY.TileProperties.TileEnum, count);

            matches.Add(tile);
        }
    }

    private void GetBottomMatches(int x, int y)
    {
        if (y - 1 < 0) return;

        int count = 1;

        TileBoard tileXY = GetTile(tiles[x, y]);

        if (x + 1 < Board.Size && x - 1 >= 0)
        {
            TileBoard tileX1Y = GetTile(tiles[x + 1, y - 1]);
            TileBoard tileXM1Y = GetTile(tiles[x - 1, y - 1]);

            if (tileX1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileXM1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum)
            {
                count += 2;

                if (x + 2 < Board.Size)
                {
                    TileBoard tileX2Y = GetTile(tiles[x + 2, y - 1]);

                    if (tileX2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }

                if (x - 2 >= 0)
                {
                    TileBoard tileXM2Y = GetTile(tiles[x - 2, y - 1]);

                    if (tileXM2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }
            }
            else
            {
                if (x + 2 < Board.Size)
                {
                    TileBoard tileX2Y = GetTile(tiles[x + 2, y - 1]);

                    if (tileX1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileX2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }

                if (x - 2 >= 0)
                {
                    TileBoard tileXM2Y = GetTile(tiles[x - 2, y - 1]);

                    if (tileXM1Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileXM2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }
            }
        }

        if (y - 3 >= 0)
        {
            TileBoard tileXY2 = GetTile(tiles[x, y - 2]);
            TileBoard tileXY3 = GetTile(tiles[x, y - 3]);

            if (tileXY2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileXY3.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
        }

        if (count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.BOTTOM, tileXY.TileProperties.TileEnum, count);

            matches.Add(tile);
        }
    }

    private void GetLeftMatches(int x, int y)
    {
        if (x - 1 < 0) return;

        int count = 1;

        TileBoard tileXY = GetTile(tiles[x, y]);

        if (y + 1 < Board.Size && y - 1 >= 0)
        {
            TileBoard tileXY1 = GetTile(tiles[x - 1, y + 1]);
            TileBoard tileXYM1 = GetTile(tiles[x - 1, y - 1]);

            if (tileXY1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileXYM1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum)
            {
                count += 2;

                if (y + 2 < Board.Size)
                {
                    TileBoard tileXY2 = GetTile(tiles[x - 1, y + 2]);

                    if (tileXY2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }

                if (y - 2 >= 0)
                {
                    TileBoard tileXYM2 = GetTile(tiles[x - 1, y - 2]);

                    if (tileXYM2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count++;
                }
            }
            else
            {
                if (y + 2 < Board.Size)
                {
                    TileBoard tileXY2 = GetTile(tiles[x - 1, y + 2]);

                    if (tileXY1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileXY2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }

                if (y - 2 >= 0)
                {
                    TileBoard tileXYM2 = GetTile(tiles[x - 1, y - 2]);

                    if (tileXYM1.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                        && tileXYM2.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
                }
            }
        }

        if (x - 3 >= 0)
        {
            TileBoard tileX2Y = GetTile(tiles[x - 2, y]);
            TileBoard tileX3Y = GetTile(tiles[x - 3, y]);

            if (tileX2Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum
                && tileX3Y.TileProperties.TileEnum == tileXY.TileProperties.TileEnum) count += 2;
        }

        if (count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.LEFT, tileXY.TileProperties.TileEnum, count);

            matches.Add(tile);
        }
    }

    public void CountTurn()
    {
        tiles = Board.BoardGen.Tiles;

        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                count = 0;

                Count(x, y);

                if (count >= 4) Battle.Instance.TurnCount++;
            }
        }
    }

    private void Count(int x, int y)
    {
        TileBoard tile = GetTile(tiles[x, y]);

        if (!tile.TileProperties.CanBeDestroyed) return;
        if (tile.TileProperties.HasCount) return;

        tile.TileProperties.HasCount = true;
        count++;

        if (y + 1 < Board.Size)
        {
            TileBoard tileXY1 = GetTile(tiles[x, y + 1]);

            if (tileXY1.TileProperties.CanBeDestroyed && !tileXY1.TileProperties.HasCount
                && tile.TileProperties.TileEnum == tileXY1.TileProperties.TileEnum)
            {
                Count(x, y + 1);
            }
        }

        if (x + 1 < Board.Size)
        {
            TileBoard tileX1Y = GetTile(tiles[x + 1, y]);

            if (tileX1Y.TileProperties.CanBeDestroyed && !tileX1Y.TileProperties.HasCount
                && tile.TileProperties.TileEnum == tileX1Y.TileProperties.TileEnum)
            {
                Count(x + 1, y);
            }
        }

        if (y - 1 >= 0)
        {
            TileBoard tileXYM1 = GetTile(tiles[x, y - 1]);

            if (tileXYM1.TileProperties.CanBeDestroyed && !tileXYM1.TileProperties.HasCount
                && tile.TileProperties.TileEnum == tileXYM1.TileProperties.TileEnum)
            {
                Count(x, y - 1);
            }
        }

        if (x - 1 >= 0)
        {
            TileBoard tileXM1Y = GetTile(tiles[x - 1, y]);

            if (tileXM1Y.TileProperties.CanBeDestroyed && !tileXM1Y.TileProperties.HasCount
                && tile.TileProperties.TileEnum == tileXM1Y.TileProperties.TileEnum)
            {
                Count(x - 1, y);
            }
        }
    }
}