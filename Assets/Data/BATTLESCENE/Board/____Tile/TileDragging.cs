using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Battle
{
    public class TileDragging : TileAb
    {
        [SerializeField] private Vector3 firstMousePos, finalMousePos;
        [SerializeField] private float angle;
        [SerializeField] private float waitTime = 1;
        [SerializeField] private bool reverse = false;

        private Transform[,] tiles;
        private Board board;
        private BoardMatches boardMatches;
        private BoardDestroyedMatches boardDestroyedMatches;

        private void OnMouseDown()
        {
            firstMousePos = to2DVec(InputManager.Instance.MousePos);

            if (!Game.Instance.TileBorder.Model.gameObject.activeSelf)
            {
                Game.Instance.TileBorder.Model.gameObject.SetActive(true);
                Game.Instance.TileBorder.IsDisplayed = true;
            }

            Game.Instance.TileBorder.transform.position = transform.parent.localPosition;
        }

        private void OnMouseUp()
        {
            if (!Battle.Instance.CanDrag) return;
            if (transform.parent.position.y >= 4.5f) return;

            finalMousePos = to2DVec(InputManager.Instance.MousePos);
            float distance = Vector3.Distance(finalMousePos, firstMousePos);

            if (distance < 0.5f) return;

            Vector3 direction = finalMousePos - firstMousePos;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Battle.Instance.CanDrag = false;
            ChangePos();
        }

        public void ChangePos()
        {
            board = Game.Instance.Board;
            tiles = board.BoardGen.Tiles;
            boardMatches = board.BoardMatches;
            boardDestroyedMatches = board.BoardDestroyedMatches;

            int tileX = Tile.TilePrefab.X;
            int tileY = Tile.TilePrefab.Y;
            Tiles tile = GetTile(tiles[tileX, tileY]);
            reverse = MoveTile(tile, tileX, tileY);

            if (reverse)
            {
                Battle.Instance.CanDrag = true;

                return;
            }

            Battle.Instance.TurnCount--;
            StartCoroutine(boardDestroyedMatches.DestroyAndFill());
        }

        private bool MoveTile(Tiles tile, int x, int y)
        {
            if (angle > 45 && angle <= 135 && y + 1 < board.Size)
            {
                if (Tile.TileMoving.MoveToTop(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            if (angle > -45 && angle <= 45 && x + 1 < board.Size)
            {
                if (Tile.TileMoving.MoveToRight(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            if (angle > -135 && angle <= -45 && y - 1 >= 0)
            {
                if (Tile.TileMoving.MoveToBottom(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            if (angle > 135 || angle <= -135 && x - 1 >= 0)
            {
                if (Tile.TileMoving.MoveToLeft(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            return false;
        }
    }
}