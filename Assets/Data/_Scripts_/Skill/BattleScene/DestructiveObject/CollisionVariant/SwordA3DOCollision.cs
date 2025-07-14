using System.Collections.Generic;
using UnityEngine;

public class SwordA3DOCollison : DestructiveObjectCollision
{
    [SerializeField] protected List<Transform> hitFXObjects;
    protected override void SpawnTileHitFX(Transform other)
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 offsetPos = new Vector3(other.transform.parent.position.x - (7 - i) - DObject.OffsetValue,
                                        other.transform.parent.position.y + DObject.OffsetValue,
                                        other.transform.parent.position.z);
            Transform hitFx = DestructiveObjectFXSpawner.Instance.Spawn(normalHitFX, offsetPos, Quaternion.identity);

            hitFx.gameObject.SetActive(true);
            hitFXObjects.Add(hitFx);
        }
    }

    protected override void DespawnFX()
    {
        foreach (Transform hitFx in hitFXObjects)
        {
            DestructiveObjectFXSpawner.Instance.Despawn(hitFx);
        }
    }
}