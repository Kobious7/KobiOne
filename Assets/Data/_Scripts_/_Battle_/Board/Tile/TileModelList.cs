using UnityEngine;

public class TileModelList : GMono
{
    [SerializeField] private TileModelPrefab[] tileModelPrefabs;

    public TileModelPrefab[] TileModelPrefabs => tileModelPrefabs;
}