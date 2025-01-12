using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TilePrefab : TileAb
    {
        [SerializeField] private SpriteRenderer sprite;
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
            set { SetTileEnum(value); }
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

        private Dictionary<TileEnum, Sprite> tilePrefabDict;

        public int TileDictLength
        {
            get { return tilePrefabDict.Count; }
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
            tilePrefabDict = new();
            TileStruct[] tileList = Tile.TileList.TileStructs;

            for (int i = 0; i < tileList.Length; i++)
            {
                if (!tilePrefabDict.ContainsKey(tileList[i].tileType))
                {
                    tilePrefabDict.Add(tileList[i].tileType, tileList[i].sprite);
                }
            }
        }

        public void SetTileEnum(TileEnum tileEnum)
        {
            this.tileEnum = tileEnum;

            if (tilePrefabDict.ContainsKey(tileEnum))
            {
                sprite.sprite = tilePrefabDict[tileEnum];
            }
        }
    }
}