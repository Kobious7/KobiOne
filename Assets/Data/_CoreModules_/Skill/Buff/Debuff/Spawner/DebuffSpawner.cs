using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebuffSpawner : Spawner
{
    [SerializeField] private int debuffIndex = 100;
    [SerializeField] private List<DebuffObject> debuffObjects;
    public event Action<DebuffObject> OnDebuffObjectChange;
    public event Action<int> OnDebuffObjectDespawn;

    public void SpawnDebuffs(int level, List<ActiveDebuff> debuffs, BEntityStats stats)
    {
        GetCurrentDebuff();

        foreach (var debuff in debuffs)
        {
            SpawnDebuff(level, debuff, stats);
        }
    }

    public void SpawnDebuff(int level, ActiveDebuff debuff, BEntityStats stats)
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
                    float percentBuff = debuff.PercentBonus * (level - 1) + debuff.DebuffPercent;
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

    public void SpawnNewDebuff(int level, ActiveDebuff debuff, BEntityStats stats)
    {
        debuffIndex = debuffIndex++ > 200 ? 100 : debuffIndex++;

        Transform newDebuff = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);

        DebuffObject objCom = newDebuff.GetComponent<DebuffObject>();
        objCom.Index = debuffIndex;
        objCom.Icon = debuff.Icon;
        objCom.Stats = stats;
        objCom.SourceStat = debuff.SourceStat;
        objCom.TrueStatBuff = debuff.StatDebuff;
        float percentBuff = debuff.PercentBonus * (level - 1) + debuff.DebuffPercent;
        objCom.PercentBuff = percentBuff;
        objCom.DurationType = debuff.DurationType;
        objCom.Duration = debuff.Duration;
        objCom.DurationStack = debuff.DurationStack;
        objCom.PercentStack = debuff.PercentStack;
        objCom.Description = debuff.Description;

        newDebuff.gameObject.SetActive(true);
        OnDebuffObjectChange?.Invoke(objCom);
    }

    public void ImmediatelyDebuffHandling(int level, ActiveBuff debuff, BEntityStats stats)
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

    public void CallOnBuffChangeEvent(DebuffObject debuff)
    {
        OnDebuffObjectChange?.Invoke(debuff);
    }

    public void CallOnBuffDespawnEvent(DebuffObject debuff)
    {

        OnDebuffObjectDespawn?.Invoke(debuff.Index);
    }
}