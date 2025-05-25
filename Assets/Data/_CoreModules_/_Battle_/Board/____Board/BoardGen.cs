using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardGen : BoardAb
{
    [SerializeField] private Transform tileB;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    private Transform[,] tiles;

    public Transform[,] Tiles => tiles;

    private TileBackgroundSpawner tileBgSpawner;
    private TileSpawner tileSpawner;

    protected override void Start()
    {
        base.Start();

        tileBgSpawner = Game.Instance.TileBackgroundSpawner;
        tileSpawner = Game.Instance.TileSpawner;
        tiles = new Transform[Board.Size, Board.Size * 2];

        GenTileBackground();
        GenBoard();
        RegenBoard();
    }

    public void GenBoard()
    {
        GenTile();
        Board.BoardFilling.Fill();
        StartCoroutine(GenFullBoard());
        Board.BoardMatches.GetListMatches();
    }

    public void RegenBoard()
    {
        while(Board.BoardMatches.Matches.Count <= 0)
        {
            tileSpawner.DespawnAllTiles();

            tiles = new Transform[Board.Size, Board.Size * 2];

            GenTile();
            Board.BoardFilling.Fill();
            StartCoroutine(GenFullBoard());
            Board.BoardMatches.GetListMatches();
        }
    }

    protected void GenTileBackground()
    {
        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                Transform newTileB = tileBgSpawner.Spawn(tileB, GetWorldPosition(x, y, 0), Quaternion.identity);

                newTileB.gameObject.SetActive(true);

            }
        }
    }

    protected void GenTile()
    {
        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = Board.Size; y < Board.Size * 2; y++)
            {
                Transform newTile = tileSpawner.Spawn(tileSpawner.TileObject, GetWorldPosition(x, y, -1), Quaternion.identity);
                //newTile.localScale = Vector3.zero;

                newTile.gameObject.SetActive(true);

                Tiles newTilePrefab = GetTile(newTile);

                newTilePrefab.TilePrefab.SetTileEnum((TileEnum)Random.Range(0, newTilePrefab.TilePrefab.TileDictLength));

                while (HasMatchesWhenGen(newTilePrefab.TilePrefab.TileEnum, x, y))
                {
                    newTilePrefab.TilePrefab.SetTileEnum((TileEnum)Random.Range(0, newTilePrefab.TilePrefab.TileDictLength));
                }

                newTilePrefab.TilePrefab.SetXY(x, y);

                tiles[x, y] = newTile;
            }
        }
    }

    public IEnumerator GenFullBoard()
    {
        yield return new WaitForSeconds(1);

        for (int y = Board.Size; y < Board.Size * 2; y++)
        {
            for (int x = 0; x < Board.Size; x++)
            {
                if (tiles[x, Board.Size * 2 - 1] == null)
                {
                    Transform newTile = tileSpawner.Spawn(tileSpawner.TileObject, Board.BoardGen.GetWorldPosition(x, Board.Size * 2 - 1, -1), Quaternion.identity);

                    newTile.gameObject.SetActive(true);

                    Tiles newTilePrefab = GetTile(newTile);

                    newTilePrefab.TilePrefab.SetTileEnum((TileEnum)Random.Range(0, newTilePrefab.TilePrefab.TileDictLength));
                    newTilePrefab.TilePrefab.SetXY(x, Board.Size * 2 - 1);

                    tiles[x, Board.Size * 2 - 1] = newTile;
                }


                if (tiles[x, y] == null)
                {
                    int yP = y;

                    while (tiles[x, yP] == null)
                    {
                        yP++;
                    }

                    Tiles tile = GetTile(tiles[x, yP]);
                    Vector3 toPos = Board.BoardGen.GetWorldPosition(x, y, -1);
                    tile.transform.position = toPos;

                    tile.TilePrefab.SetXY(x, y);

                    tiles[x, y] = tiles[x, yP];
                    tiles[x, yP] = null;
                }
            }
        }
    }

    protected bool HasMatchesWhenGen(TileEnum tileType, int x, int y)
    {
        if (x < 2 && y >= Board.Size + 2)
        {
            Tiles tileBot1 = GetTile(tiles[x, y - 1]);
            Tiles tileBot2 = GetTile(tiles[x, y - 1]);
            if (tileType == tileBot1.TilePrefab.TileEnum
                && tileType == tileBot2.TilePrefab.TileEnum)
                return true;
        }

        if (x >= 2 && y < Board.Size + 2)
        {
            Tiles tileLeft1 = GetTile(tiles[x - 1, y]);
            Tiles tileLeft2 = GetTile(tiles[x - 1, y]);

            if (tileType == tileLeft1.TilePrefab.TileEnum
                && tileType == tileLeft2.TilePrefab.TileEnum)
                return true;
        }

        if (x >= 2 && y >= Board.Size + 2)
        {
            Tiles tileBot1 = GetTile(tiles[x, y - 1]);
            Tiles tileBot2 = GetTile(tiles[x, y - 1]);
            Tiles tileLeft1 = GetTile(tiles[x - 1, y]);
            Tiles tileLeft2 = GetTile(tiles[x - 1, y]);

            if (tileType == tileBot1.TilePrefab.TileEnum
                && tileType == tileBot2.TilePrefab.TileEnum
                || tileType == tileLeft1.TilePrefab.TileEnum
                && tileType == tileLeft2.TilePrefab.TileEnum)
                return true;
        }

        return false;
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(transform.position.x - Board.Size / 2.0f + x + offsetX,
            transform.position.y - Board.Size / 2.0f + y + offsetY, z);
    }
}