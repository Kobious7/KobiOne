using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecorationSpawner : Spawner
{
    [SerializeField] private int quantity;
    [SerializeField] private float minSpacing = 10f;
    private Map map;

    protected override void Start()
    {
        base.Start();
        map = InfiniteMapManager.Instance.Map;

        map.MapSwap.OnMapSwap += SpawnDecorations;
        SpawnDecorations(MapEnum.Map0);
    }

    private void SpawnDecorations(MapEnum currentMap)
    {
        int quantityTemp = quantity;

        DespawnAllDeco();

        Transform current = currentMap == MapEnum.Map0 ? map.Maps[0] : map.Maps[1];
        List<Vector3> spawnedPositions = new();
        int attempts = 0;
        int maxAttempts = 500;

        while (quantityTemp > 0 && attempts < maxAttempts)
        {
            attempts++;

            Transform randDeco = prefabs[Random.Range(0, prefabs.Count)];
            Vector3 candidatePos = new Vector3(Random.Range(current.position.x - 245f, current.position.x + 245f),
                                                randDeco.position.y,
                                                randDeco.position.z);

            bool tooClose = spawnedPositions.Any(pos => Vector3.Distance(pos, candidatePos) < minSpacing);
            if (tooClose) continue;

            Transform deco = Spawn(randDeco, candidatePos, Quaternion.identity);
            deco.gameObject.SetActive(true);
            spawnedPositions.Add(candidatePos);

            quantityTemp--;
        }
    }

    private void DespawnAllDeco()
    {
        List<Transform> list = holder.GetComponentsInChildren<Transform>()
                                    .Where(item => item != holder.transform)
                                    .ToList();

        if(list.Count <= 0) return;

        foreach(Transform item in list)
        {
            Despawn(item);
        }
    }
}
