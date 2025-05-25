using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebuffSpawner : Spawner
{
    private static DebuffSpawner instance;
    public static DebuffSpawner Instance => instance;

    [SerializeField] private List<DebuffObject> debuffObjects;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 DebuffSpawner is allowed to exist!");

        instance = this;
    }

    public void SpawnDebuffs(int level, List<ActiveDebuff> debuffs, IEntityBattleStats stats)
    {
        GetCurrentDebuff();

        foreach(var debuff in debuffs)
        {
            SpawnDebuff(level, debuff, stats);
        }
    }

    public void SpawnDebuff(int level, ActiveDebuff debuff, IEntityBattleStats stats)
    {
        if(debuff.DurationType == DurationType.Immediately)
        {

        }
        else
        {
            if(debuff.DurationStack)
            {
                DebuffObject foundObj = FindDebuff(debuff);

                if(foundObj != null)
                {
                    foundObj.Duration += debuff.Duration;
                }
                else
                {
                    SpawnNewDebuff(level, debuff, stats);
                }
            }
            else if(debuff.PercentStack)
            {
                DebuffObject foundObj = FindDebuff(debuff);

                if(foundObj != null)
                {
                    int percentBuff = (int)(debuff.PercentBonus * (level - 1) + debuff.DebuffPercent);
                    foundObj.PercentBuff += percentBuff;
                    foundObj.DebuffHandler.DebuffHandling();
                }
                else
                {
                    SpawnNewDebuff(level, debuff, stats);
                }
            }
            else
            {
                DebuffObject foundObj = FindDebuff(debuff);

                if( foundObj != null) foundObj.Duration = debuff.Duration;
                else SpawnNewDebuff(level, debuff, stats);
            }
        }
    }

    public void SpawnNewDebuff(int level, ActiveDebuff debuff, IEntityBattleStats stats)
    {
        Transform newDebuff = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);

        DebuffObject objCom = newDebuff.GetComponent<DebuffObject>();
        objCom.Stats = stats;
        objCom.SourceStat = debuff.SourceStat;
        objCom.TrueStatBuff = debuff.StatDebuff;
        int percentBuff = (int)(debuff.PercentBonus * (level - 1) + debuff.DebuffPercent);
        objCom.PercentBuff = percentBuff;
        objCom.DurationType = debuff.DurationType;
        objCom.Duration = debuff.Duration;
        objCom.DurationStack = debuff.DurationStack;
        objCom.PercentStack = debuff.PercentStack;

        newDebuff.gameObject.SetActive(true);
    }

    public void ImmediatelyDebuffHandling(int level, ActiveBuff debuff, IEntityBattleStats stats)
    {
        int percentBuff = (int)(debuff.PercentBonus * (level - 1) + debuff.BuffPercent);

        switch(debuff.StatBuff)
        {
            case EquipStatType.CurrentHPByMaxHP:
                stats.HPDes((int)(percentBuff / 100 * stats.MaxHP));
                break;
        }
    }

    private DebuffObject FindDebuff(ActiveDebuff debuff)
    {
        foreach(var debuffObj in debuffObjects)
        {
            if(debuffObj.TrueStatBuff == debuff.StatDebuff && debuffObj.SourceStat == debuff.SourceStat && debuffObj.DurationType == debuff.DurationType)
            {
                return debuffObj;
            }
        }

        return null;
    }

    public void GetCurrentDebuff()
    {
        debuffObjects = GetComponentsInChildren<DebuffObject>().ToList();
    }

    
}