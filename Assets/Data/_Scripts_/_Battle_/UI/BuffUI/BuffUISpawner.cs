using System.Collections.Generic;
using UnityEngine;

public class BuffUISpawner : Spawner
{
    protected Dictionary<int, BuffUI> buffUIList;

    protected void UpdateBuffUI(BuffInfo buff)
    {
        if (buffUIList.ContainsKey(buff.Index))
        {
            buffUIList[buff.Index].UpdateBuffUI(buff);
        }
        else
        {
            Transform newBuffUI = Spawn(prefabs[0], transform.position, Quaternion.identity);
            newBuffUI.localScale = Vector3.one;
            BuffUI buffUIScript = newBuffUI.GetComponent<BuffUI>();

            buffUIScript.SetBuffUI(buff);

            newBuffUI.gameObject.SetActive(true);

            buffUIList.Add(buff.Index, buffUIScript);
        }
    }

    protected void DespawnBuffUI(int index)
    {
        Despawn(buffUIList[index].transform);
    }
}