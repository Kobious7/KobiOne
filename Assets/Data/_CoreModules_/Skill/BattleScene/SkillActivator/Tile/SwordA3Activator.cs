using System.Collections;
using UnityEngine;

public class SwordA3Activator : SkillActivator
{
    public override IEnumerator Activate(SkillNode skill, SkillButton button)
    {
        TileSkillSO tileSkill = (TileSkillSO)skill.skillSO;

        int objectSpawnCount = Random.Range(tileSkill.ObjectMinSpawnCount, tileSkill.ObjectMaxSpawnCount + 1);
        int tileObjects = 0, opObjects = 0;

        Debug.Log(objectSpawnCount);

        objectSpawnCount--;

        if (tileSkill.OpponentTarget)
        {
            opObjects = Random.Range(tileSkill.MinObjectToOp, tileSkill.MaxObjectToOp + 1);

            while (opObjects > objectSpawnCount)
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

        if (button == SkillButton.Q)
        {
            tile = bSkill.Q.Tile;
        }
        else if (button == SkillButton.E)
        {
            tile = bSkill.E.Tile;
        }
        else
        {
            tile = bSkill.Space.Tile;
        }

        tile.TargetsFinder.GetTileTargets(tileObjects, tileSkill.AreaString);
        playerStats.ManaDes(tileSkill.ManaCost);

        playerAnim.TileSkillCast();
        yield return StartCoroutine(playerAnim.WaitAnim("SwordRSCast"));

        for (int i = 0; i < tileObjects; i++)
        {
            Vector3 pos = tileSkill.ObjectSpawnPos[tile.TargetsFinder.TileTargets[i].GetComponent<TileBoard>().TileProperties.Y];
            Transform newObj = destructiveObjectSpawner.Spawn(tileSkill.DObjectPrefab, pos, Quaternion.identity);
            DestructiveObject destructiveObject = newObj.GetComponent<DestructiveObject>();
            destructiveObject.Target = tile.TargetsFinder.TileTargets[i];
            destructiveObject.OffsetValue = tile.TargetsFinder.OffsetValue;
            destructiveObject.HitFX = tileSkill.DObjectHitFX;

            newObj.gameObject.SetActive(true);
        }

        if (opObjects > 0)
        {
            for (int i = 0; i < opObjects; i++)
            {
                float[] seconds = { 0, 0.2f, 0, 0.4f, 0.8f, 0.5f, 0.3f, 0.4f, 0.7f, 0.8f };
                float second = seconds[Random.Range(0, seconds.Length)];

                yield return new WaitForSeconds(second);

                Transform toOpObj = destructiveObjectSpawner.Spawn(tileSkill.DObjectPrefab, playerStats.transform.parent.position, Quaternion.identity);
                toOpObj.GetComponent<DestructiveObject>().Target = tile.Monster.CenterPoint;
                toOpObj.GetComponent<DestructiveObject>().SkillButton = button;

                toOpObj.gameObject.SetActive(true);
            }
        }

        if (tileSkill.Buffs.Count > 0)
        {
            ApplyBuff(skill, playerStats, this);
        }

        if (tileSkill.Debuffs.Count > 0)
        {
            ApplyDebuff(skill, opStats);
        }

        while (destructiveObjectSpawner.TileSpawnCount > 0 || destructiveObjectSpawner.OpSpawnCount > 0)
            {
                yield return null;
            }

        if (tileSkill.TurnCount) Battle.Instance.TurnCount--;

        StartCoroutine(BattleManager.Instance.Board.BoardDestroyedMatches.SkillDestroyAndFill(tile));
    }
}