using UnityEngine;

namespace Battle
{
    public abstract class TileAb : GMono
    {
        [SerializeField] protected Tiles Tile;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTile();
        }

        private void LoadTile()
        {
            if (Tile != null) return;

            Tile = transform.parent.GetComponent<Tiles>();
        }
    }
}