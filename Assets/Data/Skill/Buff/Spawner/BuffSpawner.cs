using System.Collections.Generic;
using System.Linq;
using Battle;
using UnityEngine;

public class BuffSpawner : Spawner
{
    private static BuffSpawner instance;
    public static BuffSpawner Instance => instance;

    [SerializeField] private List<BuffObject> buffObjects;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 BuffSpawner is allowed to exist!");

        instance = this;
    }

    public void SpawnBuffs(int level, List<ActiveBuff> buffs, EntityStats stats)
    {
        GetCurrentBuff();

        foreach(var buff in buffs)
        {
            SpawnBuff(level, buff, stats);
        }
    }

    public void SpawnBuff(int level, ActiveBuff buff, EntityStats stats)
    {
        if(buff.DurationStack)
        {
            BuffObject foundObj = FindBuff(buff);

            if(foundObj != null)
            {
                foundObj.Duration += buff.Duration;
            }
            else
            {
                SpawnNewBuff(level, buff, stats);
            }
        }
        else if(buff.PercentStack)
        {
            BuffObject foundObj = FindBuff(buff);

            if(foundObj != null)
            {
                int percentBuff = (int)(buff.PercentBonus * (level - 1) + buff.BuffPercent);
                foundObj.PercentBuff += percentBuff;
                foundObj.BuffHandler.BuffHandling();
            }
            else
            {
                SpawnNewBuff(level, buff, stats);
            }
        }
        else
        {
            BuffObject foundObj = FindBuff(buff);

            if(foundObj != null) foundObj.Duration = buff.Duration;
            else SpawnNewBuff(level, buff, stats);
        }
    }

    public void SpawnNewBuff(int level, ActiveBuff buff, EntityStats stats)
    {
        Transform newBuff = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);

        BuffObject objCom = newBuff.GetComponent<BuffObject>();
        objCom.Stats = stats;
        objCom.SourceStat = buff.SourceStat;
        objCom.TrueStatBuff = buff.StatBuff;
        int percentBuff = (int)(buff.PercentBonus * (level - 1) + buff.BuffPercent);
        objCom.PercentBuff = percentBuff;
        objCom.DurationType = buff.DurationType;
        objCom.Duration = buff.Duration;
        objCom.DurationStack = buff.DurationStack;
        objCom.PercentStack = buff.PercentStack;

        newBuff.gameObject.SetActive(true);
    }

    private BuffObject FindBuff(ActiveBuff buff)
    {
        foreach(var buffObj in buffObjects)
        {
            if(buffObj.TrueStatBuff == buff.StatBuff && buffObj.SourceStat == buff.SourceStat && buffObj.DurationType == buff.DurationType)
            {
                return buffObj;
            }
        }

        return null;
    }

    public void GetCurrentBuff()
    {
        buffObjects = GetComponentsInChildren<BuffObject>().ToList();
    }
}