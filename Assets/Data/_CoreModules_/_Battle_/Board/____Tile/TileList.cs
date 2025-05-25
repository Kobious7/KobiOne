using UnityEngine;

public class TileList : GMono
{
    [SerializeField] private TileStruct[] tileStructs;

    public TileStruct[] TileStructs => tileStructs;
}