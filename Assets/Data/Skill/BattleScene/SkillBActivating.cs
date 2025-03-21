using System.Collections;
using UnityEngine;

namespace Battle
{
    public class SkillBActivating : SkillBAb
    {
        private DestructiveObjectSpawner destructiveObjectSpawner;

        protected override void Start()
        {
            base.Start();

            destructiveObjectSpawner = DestructiveObjectSpawner.Instance;
        }

        public IEnumerator TileSkillActive()
        {
            TileSkillSO tileSkill = (TileSkillSO)SkillB.QSkill.skillSO;

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

            SkillB.Q.QTile.TargetsFinder.GetTileTargets(tileObjects, tileSkill.AreaString);
            Game.Instance.Player.Stats.ManaDes(tileSkill.ManaCost);

            for(int i = 0; i < tileObjects; i++)
            {
                Transform newObj;

                if(tileSkill.AreaString == "rows")
                {
                    Vector3 pos = tileSkill.ObjectSpawnPos[SkillB.Q.QTile.TargetsFinder.TileTargets[i].GetComponent<Tiles>().TilePrefab.Y];
                    newObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), pos, Quaternion.identity);
                }
                else
                {
                    newObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), destructiveObjectSpawner.transform.position, Quaternion.identity);
                }

                newObj.GetComponent<DestructiveObject>().Target = SkillB.Q.QTile.TargetsFinder.TileTargets[i];

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
                        toOpObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), Game.Instance.Player.transform.position, Quaternion.identity);

                    }
                    else
                    {
                        toOpObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), destructiveObjectSpawner.transform.position, Quaternion.identity);
                    }

                    toOpObj.GetComponent<DestructiveObject>().Target = SkillB.Q.QTile.Opponent;

                    toOpObj.gameObject.SetActive(true);   
                }
            }

            // if(tileSkill.AnotherTargets == SkillTarget.OPPONENT || tileSkill.AnotherTargets == SkillTarget.SELFOPPONENT)
            // {

            //     if(tileSkill.AnotherTargets == SkillTarget.OPPONENT) DebuffHandling(tileSkill.Debuffs);

            //     if(tileSkill.AnotherTargets == SkillTarget.SELFOPPONENT)
            //     {
            //         DebuffHandling(tileSkill.Debuffs);
            //         BuffHandling(tileSkill.Buffs);
            //     }
            // }

            while (destructiveObjectSpawner.TileSpawnCount > 0 || destructiveObjectSpawner.OpSpawnCount > 0)
            {
                yield return null;
            }

            Battle.Instance.TurnCount--;

            StartCoroutine(Game.Instance.Board.BoardDestroyedMatches.SkillDestroyAndFill());
        }
    }
}