using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpponentPick : OpponentComponent
{
    private BoardMatches boardMatches;
    private List<TileCanBeMatches> list;

    public TileCanBeMatches Stupick()
    {
        boardMatches = Game.Instance.Board.BoardMatches;
        list = boardMatches.Matches;
        int random = Random.Range(0, list.Count - 1);

        return list[random];
    }

    public TileCanBeMatches WisePick()
    {
        boardMatches = Game.Instance.Board.BoardMatches;
        list = boardMatches.Matches;

        foreach (TileCanBeMatches tile in list)
        {
            if (tile.MatchNums >= 4)
            {
                return tile;
            }
        }

        foreach (TileCanBeMatches tile in list)
        {
            if (tile.MatchNums < 4)
            {
                return tile;
            }
        }

        return null;
    }
}