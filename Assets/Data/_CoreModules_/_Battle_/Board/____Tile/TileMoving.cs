using System.Collections;
using UnityEngine;

public class TileMoving : TileAb
{
    [SerializeField] private float speed = 1;

    public IEnumerator Moving(Vector3 to)
    {
        float counter = 0;

        while (counter < speed)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, to, counter / speed);
            counter += Time.deltaTime;

            // if(transform.parent.position.y < 4.8f)
            // {
            //     transform.parent.localScale = new Vector3(1, 1, 1);
            // }

            yield return null;
        }

        transform.parent.position = to;
    }

    public IEnumerator Moving(Vector3 to, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        float counter = 0;

        while (counter < speed)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, to, counter / speed);
            counter += Time.deltaTime;

            // if(transform.parent.position.y < 4.8f)
            // {
            //     transform.parent.localScale = new Vector3(1, 1, 1);
            // }

            yield return null;
        }

        transform.parent.position = to;
    }

    public void Moving(Transform to)
    {
        Vector3 toV3 = to.position;
        Moving(toV3);
    }

    public bool MoveToTop(Transform[,] tiles, Tiles tile, int x, int y, float waitTime, Board board, BoardMatches boardMatches)
    {
        Tiles top = GetTile(tiles[x, y + 1]);

        StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y + 1, -1)));
        StartCoroutine(top.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));

        if (!boardMatches.CanBeDestroyed(tile, x, y + 1, tiles, TileDirection.TOP)
            && !boardMatches.CanBeDestroyed(top, x, y, tiles, TileDirection.BOTTOM))
        {
            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
            StartCoroutine(top.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y + 1, -1), waitTime));
            return true;
        }

        top.TilePrefab.SetXY(x, y);
        tile.TilePrefab.SetXY(x, y + 1);

        Transform current = tiles[x, y];
        tiles[x, y] = tiles[x, y + 1];
        tiles[x, y + 1] = current;

        return false;
    }

    public bool MoveToRight(Transform[,] tiles, Tiles tile, int x, int y, float waitTime, Board board, BoardMatches boardMatches)
    {
        Tiles right = GetTile(tiles[x + 1, y]);

        StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x + 1, y, -1)));
        StartCoroutine(right.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));

        if (!boardMatches.CanBeDestroyed(tile, x + 1, y, tiles, TileDirection.RIGHT)
            && !boardMatches.CanBeDestroyed(right, x, y, tiles, TileDirection.LEFT))
        {
            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
            StartCoroutine(right.TileMoving.Moving(board.BoardGen.GetWorldPosition(x + 1, y, -1), waitTime));
            return true;
        }

        right.TilePrefab.SetXY(x, y);
        tile.TilePrefab.SetXY(x + 1, y);

        Transform current = tiles[x, y];
        tiles[x, y] = tiles[x + 1, y];
        tiles[x + 1, y] = current;

        return false;
    }

    public bool MoveToBottom(Transform[,] tiles, Tiles tile, int x, int y, float waitTime, Board board, BoardMatches boardMatches)
    {
        Tiles bottom = GetTile(tiles[x, y - 1]);

        StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y - 1, -1)));
        StartCoroutine(bottom.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));

        if (!boardMatches.CanBeDestroyed(tile, x, y - 1, tiles, TileDirection.BOTTOM)
            && !boardMatches.CanBeDestroyed(bottom, x, y, tiles, TileDirection.TOP))
        {
            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
            StartCoroutine(bottom.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y - 1, -1), waitTime));
            return true;
        }

        bottom.TilePrefab.SetXY(x, y);
        tile.TilePrefab.SetXY(x, y - 1);

        Transform current = tiles[x, y];
        tiles[x, y] = tiles[x, y - 1];
        tiles[x, y - 1] = current;

        return false;
    }

    public bool MoveToLeft(Transform[,] tiles, Tiles tile, int x, int y, float waitTime, Board board, BoardMatches boardMatches)
    {
        Tiles left = GetTile(tiles[x - 1, y]);

        StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x - 1, y, -1)));
        StartCoroutine(left.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));

        if (!boardMatches.CanBeDestroyed(tile, x - 1, y, tiles, TileDirection.LEFT)
            && !boardMatches.CanBeDestroyed(left, x, y, tiles, TileDirection.RIGHT))
        {
            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
            StartCoroutine(left.TileMoving.Moving(board.BoardGen.GetWorldPosition(x - 1, y, -1), waitTime));
            return true;
        }

        left.TilePrefab.SetXY(x, y);
        tile.TilePrefab.SetXY(x - 1, y);

        Transform current = tiles[x, y];
        tiles[x, y] = tiles[x - 1, y];
        tiles[x - 1, y] = current;

        return false;
    }
}