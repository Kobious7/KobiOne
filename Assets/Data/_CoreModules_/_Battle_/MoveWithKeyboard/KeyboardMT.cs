using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private BattleManager battleManager;

    protected override void Start()
    {
        base.Start();

        battleManager = BattleManager.Instance;
    }

    private void FixedUpdate()
    {
        CountTime();
        EnterCountTime();
        ChooseCountTime();

        if (canMove && !battleManager.TileBorder.IsEnter && (InputManager.Instance.Horizontal != 0 || InputManager.Instance.Vertical != 0))
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

        if (canChoose && battleManager.TileBorder.IsEnter && (InputManager.Instance.Horizontal != 0 || InputManager.Instance.Vertical != 0))
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
        if (!battleManager.TileBorder.IsDisplayed)
        {
            battleManager.TileSpawner.GetGeneratedTilesList();

            Transform tile = battleManager.TileSpawner.GetRandomTile().transform;

            battleManager.TileBorder.Model.gameObject.SetActive(true);
            battleManager.TileBorder.IsDisplayed = true;
            battleManager.TileBorder.transform.position = tile.position;

            return;
        }

        Vector3 currentPos = battleManager.TileBorder.transform.position;

        if (InputManager.Instance.Horizontal > 0)
        {
            currentPos.x = currentPos.x + 1 > (float)7 / 2 ? (float)-7 / 2 : currentPos.x + 1;
            battleManager.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
            return;
        }

        if (InputManager.Instance.Horizontal < 0)
        {
            currentPos.x = currentPos.x - 1 < (float)-7 / 2 ? (float)7 / 2 : currentPos.x - 1;
            battleManager.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
            return;
        }

        if (InputManager.Instance.Vertical > 0)
        {
            currentPos.y = currentPos.y + 1 > (float)7 / 2 ? (float)-7 / 2 : currentPos.y + 1;
            battleManager.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
            return;
        }

        if (InputManager.Instance.Vertical < 0)
        {
            currentPos.y = currentPos.y - 1 < (float)-7 / 2 ? (float)7 / 2 : currentPos.y - 1;
            battleManager.TileBorder.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z);
            return;
        }
    }

    private void Enter()
    {
        if (!battleManager.TileBorder.IsDisplayed)
        {
            battleManager.TileSpawner.GetGeneratedTilesList();

            Transform tile = battleManager.TileSpawner.GetRandomTile().transform;

            battleManager.TileBorder.Model.gameObject.SetActive(true);
            battleManager.TileBorder.IsDisplayed = true;
            battleManager.TileBorder.transform.position = tile.position;

            return;
        }

        if (!battleManager.TileBorder.Arrows.gameObject.activeSelf)
        {
            battleManager.TileBorder.IsEnter = true;
            battleManager.TileBorder.Arrows.gameObject.SetActive(true);
            return;
        }

        if (battleManager.TileBorder.Arrows.gameObject.activeSelf)
        {
            battleManager.TileBorder.IsEnter = false;
            battleManager.TileBorder.Arrows.gameObject.SetActive(false);
            return;
        }
    }

    public void Choose()
    {
        board = battleManager.Board;
        tiles = board.BoardGen.Tiles;
        boardMatches = board.BoardMatches;
        boardDestroyedMatches = board.BoardDestroyedMatches;
        Vector3 currentPos = battleManager.TileBorder.transform.position;
        int x = (int)(currentPos.x + 3.5f);
        int y = (int)(currentPos.y + 3.5f);
        TileBoard tile = GetTile(tiles[x, y]);
        bool reverse = MoveTileCheck(tile, x, y);

        if (reverse) return;

        Battle.Instance.TurnCount--;
        battleManager.TileBorder.Arrows.gameObject.SetActive(false);
        battleManager.TileBorder.IsEnter = false;
        StartCoroutine(boardDestroyedMatches.DestroyAndFill());
        Debug.Log("????");
    }

    private bool MoveTileCheck(TileBoard tile, int x, int y)
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