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
    private TileEnum[] randomTileTypes = { TileEnum.EXP, TileEnum.HEART, TileEnum.MANA, TileEnum.SHEILD, TileEnum.SLASH, TileEnum.SWORD, TileEnum.VHEART };

    protected override void Start()
    {
        base.Start();

        tileBgSpawner = BattleManager.Instance.TileBackgroundSpawner;
        tileSpawner = BattleManager.Instance.TileSpawner;
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
                Transform newTileB = tileBgSpawner.Spawn(tileB, GetWorldPosition(x, y, 2), Quaternion.identity);

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
                TileEnum randomTileType = randomTileTypes[Random.Range(0, randomTileTypes.Length)];

                while (HasMatchesWhenGen(randomTileType, x, y))
                {
                    randomTileType = randomTileTypes[Random.Range(0, randomTileTypes.Length)];
                }

                Transform newTile = tileSpawner.SpawnTilePrefab(randomTileType, GetWorldPosition(x, y, 1), Quaternion.identity);
                TileBoard newTileBoard = GetTile(newTile);

                newTileBoard.TileProperties.SetXY(x, y);
                newTile.gameObject.SetActive(true);

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
                    TileEnum randomTileType = randomTileTypes[Random.Range(0, randomTileTypes.Length)];
                    Transform newTile = tileSpawner.SpawnTilePrefab(randomTileType, Board.BoardGen.GetWorldPosition(x, Board.Size * 2 - 1, 0), Quaternion.identity);
                    TileBoard newTileBoard = GetTile(newTile);

                    newTileBoard.TileProperties.SetXY(x, Board.Size * 2 - 1);
                    newTile.gameObject.SetActive(true);

                    tiles[x, Board.Size * 2 - 1] = newTile;
                }


                if (tiles[x, y] == null)
                {
                    int yP = y;

                    while (tiles[x, yP] == null)
                    {
                        yP++;
                    }

                    TileBoard tile = GetTile(tiles[x, yP]);
                    Vector3 toPos = GetWorldPosition(x, y, 1);
                    tile.transform.position = toPos;

                    tile.TileProperties.SetXY(x, y);

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
            TileBoard tileBot1 = GetTile(tiles[x, y - 1]);
            TileBoard tileBot2 = GetTile(tiles[x, y - 1]);
            if (tileType == tileBot1.TileProperties.TileEnum
                && tileType == tileBot2.TileProperties.TileEnum)
                return true;
        }

        if (x >= 2 && y < Board.Size + 2)
        {
            TileBoard tileLeft1 = GetTile(tiles[x - 1, y]);
            TileBoard tileLeft2 = GetTile(tiles[x - 1, y]);

            if (tileType == tileLeft1.TileProperties.TileEnum
                && tileType == tileLeft2.TileProperties.TileEnum)
                return true;
        }

        if (x >= 2 && y >= Board.Size + 2)
        {
            TileBoard tileBot1 = GetTile(tiles[x, y - 1]);
            TileBoard tileBot2 = GetTile(tiles[x, y - 1]);
            TileBoard tileLeft1 = GetTile(tiles[x - 1, y]);
            TileBoard tileLeft2 = GetTile(tiles[x - 1, y]);

            if (tileType == tileBot1.TileProperties.TileEnum
                && tileType == tileBot2.TileProperties.TileEnum
                || tileType == tileLeft1.TileProperties.TileEnum
                && tileType == tileLeft2.TileProperties.TileEnum)
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