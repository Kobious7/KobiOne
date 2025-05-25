using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDestroyedMatches : BoardAb
{
    private Transform[,] tiles;
    private IEnumerator destroy;
    private TileSpawner tileSpawner;

    public IEnumerator DestroyAndFill()
    {

        yield return new WaitForSeconds(1);

        tiles = Board.BoardGen.Tiles;

        Board.BoardMatches.MarkAsMatches(tiles);
        Board.BoardMatches.CountTurn();
        Battle.Instance.NewTileCounter();
        StartDestroy();
    }

    public IEnumerator SkillDestroyAndFill(TileSkill tileSkill)
    {
        yield return new WaitForSeconds(1);

        tiles = Board.BoardGen.Tiles;

        Board.BoardMatches.SkillsMarkMatches(tileSkill, tiles);
        Battle.Instance.NewTileCounter();
        StartDestroy();
    }

    public void StartDestroy()
    {
        destroy = DestroyMatches();
        StartCoroutine(destroy);
    }

    public IEnumerator DestroyMatches()
    {
        tiles = Board.BoardGen.Tiles;

        DestroyM();
        Board.BoardFilling.Fill();
        StartCoroutine(Board.BoardGen.GenFullBoard());
        Board.BoardMatches.MarkAsMatches(tiles);
        Board.BoardMatches.CountTurn();

        while (HasMatches())
        {
            yield return new WaitForSeconds(1);
            if (destroy != null) StopCoroutine(destroy);

            destroy = DestroyMatches();

            StartCoroutine(destroy);
        }

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Battle.Instance.TileHandling());

        Board.BoardMatches.GetListMatches();

        Board.BoardGen.RegenBoard();

        Battle.Instance.TurnChange();
    }

    public bool HasMatches()
    {
        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                Tiles t = GetTile(tiles[x, y]);

                if (t.TilePrefab.CanBeDestroyed) return true;
            }
        }

        return false;
    }

    public void DestroyM()
    {
        tileSpawner = Game.Instance.TileSpawner;

        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                Tiles t = GetTile(tiles[x, y]);

                if (t.TilePrefab.CanBeDestroyed)
                {
                    if (Battle.Instance.TileCounter.ContainsKey(t.TilePrefab.TileEnum)) Battle.Instance.TileCounter[t.TilePrefab.TileEnum]++;
                    Transform fx = Game.Instance.FXSpawner.Spawn(Game.Instance.FXSpawner.FX.transform, t.transform.position, Quaternion.identity);
                    fx.gameObject.SetActive(true);
                    tileSpawner.Despawn(tiles[x, y]);

                    tiles[x, y] = null;
                }
            }
        }
    }
}