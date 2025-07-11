using UnityEngine;

public class BoardFilling : BoardAb
{
    private Transform[,] tiles;

    public void Fill()
    {
        tiles = Board.BoardGen.Tiles;

        for (int x = 0; x < Board.Size; x++)
        {
            for (int y = 0; y < Board.Size; y++)
            {
                FillTile(x, y);
            }
        }
    }

    public void FillTile(int x, int y)
    {
        if (tiles[x, y] == null)
        {
            int yP = y;
            while (tiles[x, yP] == null)
            {
                yP++;
            }

            TileBoard tile = GetTile(tiles[x, yP]);
            Vector3 toPos = Board.BoardGen.GetWorldPosition(x, y, 1);
            tile.TileProperties.Sprite.enabled = true;
            StartCoroutine(tile.TileMoving.Moving(toPos));

            tile.TileProperties.SetXY(x, y);
            tiles[x, y] = tiles[x, yP];
            tiles[x, yP] = null;
        }
    }
}