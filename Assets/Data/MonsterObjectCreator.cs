using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectCreator : GMono
{
    [SerializeField] protected List<Transform> prefabs;

    public List<Transform> Prefabs => prefabs;

    public void SpawnMonsterObject(string monsterName)
    {
        Transform monsterPrefab = GetPrefabByName(monsterName);

        Transform newMonster = Instantiate(monsterPrefab);
        newMonster.name = "Monster";
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