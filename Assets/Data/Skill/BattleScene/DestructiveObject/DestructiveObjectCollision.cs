using UnityEngine;

namespace Battle
{
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

                        int finalDamage = playerStats.DamageCalculate(rawSkillDamage, opStats);
                        
                        opStats.HPDes(finalDamage);
                        Debug.Log("Skill Damage");
                        skillB.SkillActivator.ApplyDebuff(skillB.QSkill, opStats);
                    }

                    if(transform.parent.name == "E")
                    {
                        int rawSkillDamage = skillB.Calculator.SkillDamageCalculate(playerStats, skillB.ESkill);

                        int finalDamage = playerStats.DamageCalculate(rawSkillDamage, opStats);
                        
                        opStats.HPDes(finalDamage);
                        Debug.Log("Skill Damage");
                        skillB.SkillActivator.ApplyDebuff(skillB.ESkill, opStats);
                    }

                    if(transform.parent.name == "Space")
                    {
                        int rawSkillDamage = skillB.Calculator.SkillDamageCalculate(playerStats, skillB.SpaceSkill);

                        int finalDamage = playerStats.DamageCalculate(rawSkillDamage, opStats);
                        
                        opStats.HPDes(finalDamage);
                        Debug.Log("Skill Damage");
                        skillB.SkillActivator.ApplyDebuff(skillB.SpaceSkill, opStats);
                    }

                    Battle.Instance.PlayerNextDamage = DamageType.SlashDamage;
                }
            }
        }
    }
}