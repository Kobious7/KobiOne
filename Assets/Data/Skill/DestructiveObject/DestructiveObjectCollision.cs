using Battle;
using UnityEngine;

public class DestructiveObjectCollision : DestructiveObjectAb
{
    private void OnTriggerEnter(Collider other)
    {
        if (Skills.Instance.QSkill is TileSkillSO && other.transform.parent.name == "Tile")
        {
            TilePrefab tile = other.transform.parent.GetComponentInChildren<TilePrefab>();
            TilePrefab target = DObject.Target.GetComponentInChildren<TilePrefab>();
            
            if (tile.X == target.X && tile.Y == target.Y)
            {
                DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                DestructiveObjectSpawner.Instance.SpawnedCount--;
            }
        }

        if(other.transform.name == "Opponent")
        {
            DestructiveObjectSpawner.Instance.Despawn(transform.parent);

            if(Skills.Instance.QSkill is TileSkillSO tileSkill) Game.Instance.Bot.Stats.HPDes(tileSkill.Damage);
            if(Skills.Instance.QSkill is OpSkillSO opSkillSO) Game.Instance.Bot.Stats.HPDes(opSkillSO.Damage);
        }
    }
}