using UnityEngine;

namespace Battle
{
    public class TileList : GMono
    {
        [SerializeField] private TileStruct[] tileStructs;

        public TileStruct[] TileStructs => tileStructs;
    }
}