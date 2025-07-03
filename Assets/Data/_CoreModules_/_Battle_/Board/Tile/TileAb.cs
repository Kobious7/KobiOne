using UnityEngine;

public abstract class TileBoardAb : GMono
{
    [SerializeField] protected TileBoard Tile;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTile();
    }

    private void LoadTile()
    {
        if (Tile != null) return;

        Tile = transform.parent.GetComponent<TileBoard>();
    }
}