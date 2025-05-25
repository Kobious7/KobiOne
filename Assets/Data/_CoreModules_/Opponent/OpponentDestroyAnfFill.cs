using System.Collections;
using UnityEngine;

public class OpponentDestroyAndFill : OpponentComponent
{
    private BoardDestroyedMatches boardDestroyedMatches;

    public void DestroyAndFill()
    {
        Opponent.OpponentMoveTile.MoveTile();

        boardDestroyedMatches = Game.Instance.Board.BoardDestroyedMatches;

        StartCoroutine(boardDestroyedMatches.DestroyAndFill());
    }

}