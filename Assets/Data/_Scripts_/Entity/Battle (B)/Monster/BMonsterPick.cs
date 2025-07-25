using System.Collections.Generic;
using UnityEngine;

public class BMonsterPick : BEntityComponent
{
    private BoardMatches boardMatches;
    private List<TileCanBeMatches> list;

    public TileCanBeMatches Stupick()
    {
        boardMatches = BattleManager.Instance.Board.BoardMatches;
        list = boardMatches.Matches;
        int random = Random.Range(0, list.Count - 1);

        return list[random];
    }

    public TileCanBeMatches WisePick()
    {
        boardMatches = BattleManager.Instance.Board.BoardMatches;
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