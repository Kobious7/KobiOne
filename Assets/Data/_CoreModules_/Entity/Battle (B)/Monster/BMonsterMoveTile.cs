using UnityEngine;

public class BMonsterMoveTile : BEntityComponent
{
    private Transform[,] tiles;
    private Board board;
    private BoardMatches boardMatches;
    private float waitTime = 1;
    private BMonster monster;
    private BattleManager battleManager;

    protected override void Start()
    {
        base.Start();

        battleManager = BattleManager.Instance;
        monster = Entity as BMonster;
    }

    public void MoveTile()
    {
        board = battleManager.Board;
        tiles = board.BoardGen.Tiles;
        boardMatches = board.BoardMatches;
        TileCanBeMatches pickedTile = monster.Pick.WisePick();
        int x = pickedTile.X;
        int y = pickedTile.Y;
        TileDirection direction = pickedTile.TileDirection;
        Tiles tile = GetTile(tiles[x, y]);

        if (!battleManager.TileBorder.Model.gameObject.activeSelf)
        {
            battleManager.TileBorder.Model.gameObject.SetActive(true);
            battleManager.TileBorder.IsDisplayed = true;
        }

        battleManager.TileBorder.transform.position = tiles[x, y].position;

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