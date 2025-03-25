using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SkillBActivating : SkillBAb
    {
        private DestructiveObjectSpawner destructiveObjectSpawner;
        private EntityStats playerStats;
        private EntityStats opStats;

        protected override void Start()
        {
            base.Start();

            destructiveObjectSpawner = DestructiveObjectSpawner.Instance;
            playerStats = Game.Instance.Player.Stats;
            opStats = Game.Instance.Bot.Stats;
        }

        public void Active(SkillNode skill, SkillButton button)
        {
            if(skill.skillSO is TileSkillSO) StartCoroutine(TileSkillActive(skill, button));
            else if(skill.skillSO is SelfSkillSO) SelfSkillACtive(skill, button);
            else OpSkillActive(skill, button);
        }

        private IEnumerator TileSkillActive(SkillNode skill, SkillButton button)
        {
            TileSkillSO tileSkill = (TileSkillSO)skill.skillSO;

            int objectSpawnCount = Random.Range(tileSkill.ObjectMinSpawnCount, tileSkill.ObjectMaxSpawnCount + 1);
            int tileObjects = 0, opObjects = 0;

            Debug.Log(objectSpawnCount);

            objectSpawnCount--;

            if(tileSkill.OpponentTarget)
            {
                opObjects = Random.Range(tileSkill.MinObjectToOp, tileSkill.MaxObjectToOp + 1);

                while(opObjects > objectSpawnCount)
                {
                    opObjects = Random.Range(tileSkill.MinObjectToOp, tileSkill.MaxObjectToOp + 1);
                }
            }

            tileObjects = objectSpawnCount - opObjects + 1;

            Debug.Log(tileObjects);
            Debug.Log(opObjects);

            destructiveObjectSpawner.TileSpawnCount = tileObjects;
            destructiveObjectSpawner.OpSpawnCount = opObjects;

            TileSkill tile;
            string prefabName;

            if(button == SkillButton.Q)
            {
                tile = SkillB.Q.Tile;
                prefabName = "Q";
            }
            else if(button == SkillButton.E)
            {
                tile = SkillB.E.Tile;
                prefabName = "E";
            }
            else
            {
                tile = SkillB.Space.Tile;
                prefabName = "Space";
            }
  
            tile.TargetsFinder.GetTileTargets(tileObjects, tileSkill.AreaString);
            Game.Instance.Player.Stats.ManaDes(tileSkill.ManaCost);

            for(int i = 0; i < tileObjects; i++)
            {
                Transform newObj;

                if(tileSkill.AreaString == "rows")
                {
                    Vector3 pos = tileSkill.ObjectSpawnPos[tile.TargetsFinder.TileTargets[i].GetComponent<Tiles>().TilePrefab.Y];
                    newObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName(prefabName), pos, Quaternion.identity);
                }
                else
                {
                    newObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName(prefabName), destructiveObjectSpawner.transform.position, Quaternion.identity);
                }

                DestructiveObject destructiveObject = newObj.GetComponent<DestructiveObject>();
                destructiveObject.Target = tile.TargetsFinder.TileTargets[i];
                destructiveObject.MainTarget = MainTargetType.Tile;

                newObj.gameObject.SetActive(true);
            }

            if(opObjects > 0)
            {
                for(int i = 0; i < opObjects; i++)
                {
                    float[] seconds = {0, 0.2f, 0, 0.4f, 0.8f, 0.5f, 0.3f, 0.4f, 0.7f, 0.8f};
                    float second = seconds[Random.Range(0, seconds.Length)];

                    Transform toOpObj;

                    yield return new WaitForSeconds(second);

                    if(tileSkill.AreaString == "rows")
                    {
                        toOpObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName(prefabName), playerStats.transform.parent.position, Quaternion.identity);

                    }
                    else
                    {
                        toOpObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName(prefabName), destructiveObjectSpawner.transform.position, Quaternion.identity);
                    }

                    toOpObj.GetComponent<DestructiveObject>().Target = tile.Opponent;

                    toOpObj.gameObject.SetActive(true);   
                }
            }

            if(tileSkill.Buffs.Count > 0)
            {
                ApplyBuff(skill, playerStats);
            }

            while (destructiveObjectSpawner.TileSpawnCount > 0 || destructiveObjectSpawner.OpSpawnCount > 0)
            {
                yield return null;
            }

            if(tileSkill.TurnCount) Battle.Instance.TurnCount--;

            StartCoroutine(Game.Instance.Board.BoardDestroyedMatches.SkillDestroyAndFill(tile));
        }

        private void SelfSkillACtive(SkillNode skill, SkillButton button)
        {
            SelfSkillSO selfSkill = (SelfSkillSO)skill.skillSO;

            playerStats.ManaDes(selfSkill.ManaCost);

            ApplyBuff(skill, playerStats);

            if(selfSkill.Opponent && selfSkill.Debuffs.Count > 0)
            {
                ApplyDebuff(skill, opStats);
            }

            if(selfSkill.TurnCount)
            {
                Battle.Instance.TurnCount--;
                Battle.Instance.TurnChange();
            }
        }

        private void OpSkillActive(SkillNode skill, SkillButton button)
        {
            OpSkillSO opSkill = (OpSkillSO)skill.skillSO;

            playerStats.ManaDes(opSkill.ManaCost);

            int rawDamage = SkillB.Calculator.SkillDamageCalculate(playerStats, skill);
            int finalDamage = playerStats.DamageCalculate(rawDamage, opStats);

            Game.Instance.Bot.Stats.HPDes(finalDamage);

            if(opSkill.Debuffs.Count > 0)
            {
                ApplyBuff(skill, opStats);
            }

            if(opSkill.Self && opSkill.Buffs.Count > 0)
            {
                ApplyBuff(skill, playerStats);
            }

            if(opSkill.TurnCount)
            {
                Battle.Instance.TurnCount--;
                Battle.Instance.TurnChange();
            }

        }

        public void ApplyDebuff(SkillNode skill, EntityStats stats)
        {
            int level = skill.Level;
            List<ActiveDebuff> debuffs = new();

            if(skill.skillSO is TileSkillSO tileSkill)
            {
                debuffs = tileSkill.Debuffs;
            }

            if(skill.skillSO is SelfSkillSO selfSkill)
            {
                debuffs = selfSkill.Debuffs;
            }

            if(skill.skillSO is OpSkillSO opSkill)
            {
                debuffs = opSkill.Debuffs;
            }

            DebuffSpawner.Instance.SpawnDebuffs(level, debuffs, stats);
        }

        public void ApplyBuff(SkillNode skill, EntityStats stats)
        {
            int level = skill.Level;
            List<ActiveBuff> buffs = new();

            if(skill.skillSO is TileSkillSO tileSkill)
            {
                buffs = tileSkill.Buffs;
            }

            if(skill.skillSO is SelfSkillSO selfSkill)
            {
                buffs = selfSkill.Buffs;
            }

            if(skill.skillSO is OpSkillSO opSkill)
            {
                buffs = opSkill.Buffs;
            }

            BuffSpawner.Instance.SpawnBuffs(level, buffs, stats);
        }
    }
}