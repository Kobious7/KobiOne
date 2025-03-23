using Battle;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DestructiveObjectCollision : DestructiveObjectAb
{
    private SkillB skillB;
    private EntityStats playerStats;
    private EntityStats opStats;

    protected override void Start()
    {
        base.Start();

        skillB = SkillB.Instance;
        playerStats = Game.Instance.Player.Stats;
        opStats = Game.Instance.Bot.Stats;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(DObject.Target.name == "Tile")
        {
            if (other.transform.parent.name == "Tile")
            {
                TilePrefab tile = other.transform.parent.GetComponentInChildren<TilePrefab>();
                TilePrefab target = DObject.Target.GetComponentInChildren<TilePrefab>();
                
                if (tile.X == target.X && tile.Y == target.Y)
                {
                    DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                    DestructiveObjectSpawner.Instance.TileSpawnCount--;
                }
            }
        }

        if(DObject.Target.name == "Opponent")
        {
            if(other.transform.name == "Opponent")
            {
                DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                DestructiveObjectSpawner.Instance.OpSpawnCount--;

                if(transform.parent.name == "Q")
                {
                    int rawSkillDamage = skillB.Calculator.SkillDamageCalculate(playerStats, skillB.QSkill);

                    playerStats.DamageCalculate(rawSkillDamage, opStats);
                    Debug.Log("Skill Damage");
                    skillB.SkillActivator.ApplyDebuff(skillB.QSkill, opStats);
                }
            }
        }
    }
}