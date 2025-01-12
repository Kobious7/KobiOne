using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Battle
{
    public class KeyboardMT : GMono
    {
        [SerializeField] private float timer = 0.4f;
        [SerializeField] private float counter = 0;
        [SerializeField] private float enterTimer = 0.2f;
        [SerializeField] private float enterCounter = 0;
        [SerializeField] private bool canMove;
        [SerializeField] private bool enter;
        [SerializeField] private float chooseTimer = 0.5f;
        [SerializeField] private float chooseCounter = 0;
        [SerializeField] private bool canChoose;
        private Transform[,] tiles;
        private Board board;
        private BoardMatches boardMatches;
        private BoardDestroyedMatches boardDestroyedMatches;
        private float waitTime = 1;

        private void FixedUpdate()
        {
            CountTime();
            EnterCountTime();
            ChooseCountTime();

            if (canMove && !Game.Instance.TileBorder.IsEnter && (InputManager.Instance.Horizontal != 0 || InputManager.Instance.Vertical != 0))
            {
                counter = 0;
                canMove = false;
                MoveTile();
            }

            if (enter && InputManager.Instance.EnterPressed)
            {
                enterCounter = 0;
                enter = false;
                Enter();
            }

            if (canChoose && Game.Instance.TileBorder.IsEnter && (InputManager.Instance.Horizontal != 0 || InputManager.Instance.Vertical != 0))
            {
                chooseCounter = 0;
                canChoose = false;
                Choose();
            }
        }

        private void CountTime()
        {
            if (counter >= timer)
            {
                canMove = true;
                return;
            }

            counter += Time.deltaTime;
        }

        private void EnterCountTime()
        {
            if (enterCounter >= enterTimer)
            {
                enter = true;
                return;
            }

            enterCounter += Time.deltaTime;
        }

        private void ChooseCountTime()
        {
            if (chooseCounter >= chooseTimer)
            {
                canChoose = true;
                return;
            }

            chooseCounter += Time.deltaTime;
        }

        private void MoveTile()
        {
            if (!Game.Instance.TileBorder.IsDisplayed)
            {
                Game.Instance.TileSpawner.GetGeneratedTilesList();

                Transform tile = Game.Instance.TileSpawner.GetRandomTile().transform;

                Game.Instance.TileBorder.Model.gameObject.SetActive(true);
                Game.Instance.TileBorder.IsDisplayed = true;
                Game.Instance.TileBorder.transform.position = tile.position;

                return;
            }

            Vector3 currentPos = Game.Instance.TileBorder.transform.position;

            if (InputManager.Instance.Horizontal > 0)
            {
                currentPos.x = currentPos.x + 1 > (float)7 / 2 ? (float)-7 / 2 : currentPos.x + 1;
                Game.Instance.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
                return;
            }

            if (InputManager.Instance.Horizontal < 0)
            {
                currentPos.x = currentPos.x - 1 < (float)-7 / 2 ? (float)7 / 2 : currentPos.x - 1;
                Game.Instance.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
                return;
            }

            if (InputManager.Instance.Vertical > 0)
            {
                currentPos.y = currentPos.y + 1 > (float)7 / 2 ? (float)-7 / 2 : currentPos.y + 1;
                Game.Instance.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
                return;
            }

            if (InputManager.Instance.Vertical < 0)
            {
                currentPos.y = currentPos.y - 1 < (float)-7 / 2 ? (float)7 / 2 : currentPos.y - 1;
                Game.Instance.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
                return;
            }
        }

        private void Enter()
        {
            if (!Game.Instance.TileBorder.IsDisplayed)
            {
                Game.Instance.TileSpawner.GetGeneratedTilesList();

                Transform tile = Game.Instance.TileSpawner.GetRandomTile().transform;

                Game.Instance.TileBorder.Model.gameObject.SetActive(true);
                Game.Instance.TileBorder.IsDisplayed = true;
                Game.Instance.TileBorder.transform.position = tile.position;

                return;
            }

            if (!Game.Instance.TileBorder.Arrows.gameObject.activeSelf)
            {
                Game.Instance.TileBorder.IsEnter = true;
                Game.Instance.TileBorder.Arrows.gameObject.SetActive(true);
                return;
            }

            if (Game.Instance.TileBorder.Arrows.gameObject.activeSelf)
            {
                Game.Instance.TileBorder.IsEnter = false;
                Game.Instance.TileBorder.Arrows.gameObject.SetActive(false);
                return;
            }
        }

        public void Choose()
        {
            board = Game.Instance.Board;
            tiles = board.BoardGen.Tiles;
            boardMatches = board.BoardMatches;
            boardDestroyedMatches = board.BoardDestroyedMatches;
            Vector3 currentPos = Game.Instance.TileBorder.transform.position;
            int x = (int)(currentPos.x + 3.5f);
            int y = (int)(currentPos.y + 3.5f);
            Tiles tile = GetTile(tiles[x, y]);
            bool reverse = MoveTileCheck(tile, x, y);

            if (reverse) return;

            Battle.Instance.TurnCount--;
            Game.Instance.TileBorder.Arrows.gameObject.SetActive(false);
            Game.Instance.TileBorder.IsEnter = false;
            StartCoroutine(boardDestroyedMatches.DestroyAndFill());
            Debug.Log("????");
        }

        private bool MoveTileCheck(Tiles tile, int x, int y)
        {
            if (InputManager.Instance.Vertical > 0 && y + 1 < 8)
            {
                if (tile.TileMoving.MoveToTop(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            if (InputManager.Instance.Horizontal > 0 && x + 1 < 8)
            {
                if (tile.TileMoving.MoveToRight(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            if (InputManager.Instance.Vertical < 0 && y - 1 >= 0)
            {
                if (tile.TileMoving.MoveToBottom(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            if (InputManager.Instance.Horizontal < 0 && x - 1 >= 0)
            {
                if (tile.TileMoving.MoveToLeft(tiles, tile, x, y, waitTime, board, boardMatches)) return true;
            }

            return false;
        }

    }
}