using UnityEngine;

public class OpponentMoveTile : OpponentComponent
{
    private Transform[,] tiles;
    private Board board;
    private BoardMatches boardMatches;
    private float waitTime = 1;

    public void MoveTile()
    {
        board = Game.Instance.Board;
        tiles = board.BoardGen.Tiles;
        boardMatches = board.BoardMatches;
        TileCanBeMatches pickedTile = Opponent.OpponentPick.WisePick();
        int x = pickedTile.X;
        int y = pickedTile.Y;
        TileDirection direction = pickedTile.TileDirection;
        Tiles tile = GetTile(tiles[x, y]);

        if (!Game.Instance.TileBorder.Model.gameObject.activeSelf)
        {
            Game.Instance.TileBorder.Model.gameObject.SetActive(true);
            Game.Instance.TileBorder.IsDisplayed = true;
        }

        Game.Instance.TileBorder.transform.position = tiles[x, y].position;

        if (direction == TileDirection.TOP)
        {
            tile.TileMoving.MoveToTop(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        if (direction == TileDirection.RIGHT)
        {
            tile.TileMoving.MoveToRight(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        if (direction == TileDirection.BOTTOM)
        {
            tile.TileMoving.MoveToBottom(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        if (direction == TileDirection.LEFT)
        {
            tile.TileMoving.MoveToLeft(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        Battle.Instance.TurnCount--;
    }
}