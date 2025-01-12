using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class TileCanBeMatches
    {
        [SerializeField] private int x;
        [SerializeField] private int y;
        [SerializeField] private TileDirection tileDirection;

        [SerializeField] private TileEnum tileType;

        [SerializeField] private int matchedNums;


        public TileCanBeMatches(int x, int y, TileDirection tileDirection, TileEnum tileType, int matchedNums)
        {
            this.x = x;
            this.y = y;
            this.tileDirection = tileDirection;
            this.tileType = tileType;
            this.matchedNums = matchedNums;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public TileDirection TileDirection
        {
            get { return tileDirection; }
            set { tileDirection = value; }
        }

        public TileEnum TileType
        {
            get { return tileType; }
            set { tileType = value; }
        }

        public int MatchNums
        {
            get { return matchedNums; }
            set { matchedNums = value; }
        }
    }
}