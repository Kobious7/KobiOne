using System.Collections.Generic;
using UnityEngine;

public class TileProperties : TileBoardAb
{
    [SerializeField] private SpriteRenderer sprite;

    public SpriteRenderer Sprite => sprite;
    
    [SerializeField] private TileEnum tileEnum;
    [SerializeField] private int x;

    public int X => x;

    [SerializeField] private int y;

    public int Y => y;

    [SerializeField] private int preX;
    [SerializeField] private int preY;
    [SerializeField] private bool canBeDestroyed = false;
    [SerializeField] private bool hasCount = false;

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
        preX = x;
        preY = y;
    }

    public TileEnum TileEnum
    {
        get { return tileEnum; }
        set { tileEnum = value; }
    }

    public bool CanBeDestroyed
    {
        get { return canBeDestroyed; }
        set { canBeDestroyed = value; }
    }

    public bool HasCount
    {
        get { return hasCount; }
        set { hasCount = value; }
    }

    protected override void OnDisable()
    {
        canBeDestroyed = false;
        hasCount = false;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        sprite = GetComponent<SpriteRenderer>();
    }
}