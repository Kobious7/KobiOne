using System.Collections.Generic;
using UnityEngine;

public class MapObjects : MapAb
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform map0, map1;
    [SerializeField] private MapEnum currentMap;
    [SerializeField] private List<Transform> monstersMap1, monstersMap2;

    protected override void Start()
    {
        base.Start();
        Map.MapReset.OnMapReset += ResetObjects;
    }

    private void ResetObjects()
    {
        player = Game.Instance.Player.transform;
        map0 = Map.Maps[0];
        map1 = Map.Maps[1];
        currentMap = Map.MapSwap.CurrentMap;
        monstersMap1 = Game.Instance.Map1MonsterSpawner.GetActiveMonster();
        monstersMap2 = Game.Instance.Map2MonsterSpawner.GetActiveMonster();
        Vector3 map0Pos = new Vector3(250, 0, 0);
        Vector3 map1Pos = new Vector3(-250, 0, 0);

        Vector3 offsetPlayer = currentMap == MapEnum.Map0 ? to2DVec(player.position) - map0.position : to2DVec(player.position) - map1.position;

        if (currentMap == MapEnum.Map0) player.position = offsetPlayer + map0Pos;
        else player.position = offsetPlayer + map1Pos;

        foreach (Transform monster in monstersMap1)
        {
            Vector3 offsetMonster = currentMap == MapEnum.Map0 ? to2DVec(monster.position) - map0.position : to2DVec(monster.position) - map1.position;

            if (currentMap == MapEnum.Map0) monster.position = offsetMonster + map0Pos;
            else monster.position = offsetMonster + map1Pos;
        }

        foreach (Transform monster in monstersMap2)
        {
            Vector3 offsetMonster = currentMap == MapEnum.Map0 ? to2DVec(monster.position) - map0.position : to2DVec(monster.position) - map1.position;

            if (currentMap == MapEnum.Map0) monster.position = offsetMonster + map0Pos;
            else monster.position = offsetMonster + map1Pos;
        }

        map0.position = map0Pos;
        map1.position = map1Pos;
        Map.MapSwap.CanSwap = true;
    }
}
