using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectCreator : GMono
{
    [SerializeField] protected List<Transform> prefabs;

    public List<Transform> Prefabs => prefabs;

    public void SpawnMonsterObject(string monsterName, MonsterTier tier)
    {
        Transform monsterPrefab = GetPrefabByName(monsterName);
        if (monsterPrefab == null) monsterPrefab = GetPrefabByName("Sphery");

        Transform newMonster = Instantiate(monsterPrefab);
        newMonster.name = "Monster";
        BMonster monsterCom = newMonster.GetComponentInChildren<BMonster>();

        if (tier == MonsterTier.Elite) monsterCom.RigModel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        if (tier == MonsterTier.Rampage) monsterCom.RigModel.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    private Transform GetPrefabByName(string name)
    {
        foreach (Transform prefab in prefabs)
        {
            if (prefab.name == name)
            {
                return prefab;
            }
        }

        Debug.Log("Cant find prefab name!");

        return null;
    }
}