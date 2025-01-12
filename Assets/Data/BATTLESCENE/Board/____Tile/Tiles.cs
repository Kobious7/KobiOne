using UnityEngine;

namespace Battle
{
    public class Tiles : GMono
    {
        [SerializeField] private TilePrefab tilePrefab;

        public TilePrefab TilePrefab => tilePrefab;

        [SerializeField] private TileList tileList;

        public TileList TileList => tileList;

        [SerializeField] private TileMoving tileMoving;

        public TileMoving TileMoving => tileMoving;

        [SerializeField] private TileDragging tileDragging;

        public TileDragging TileDragging => tileDragging;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTilePrefab();
            LoadTileList();
            LoadTileMoving();
            LoadTileDragging();
        }

        private void LoadTilePrefab()
        {
            if (tilePrefab != null) return;

            tilePrefab = GetComponentInChildren<TilePrefab>();
        }

        private void LoadTileList()
        {
            if (tileList != null) return;

            tileList = GetComponentInChildren<TileList>();
        }

        private void LoadTileMoving()
        {
            if (tileMoving != null) return;

            tileMoving = GetComponentInChildren<TileMoving>();
        }

        private void LoadTileDragging()
        {
            if (tileDragging != null) return;

            tileDragging = GetComponentInChildren<TileDragging>();
        }
    }
}