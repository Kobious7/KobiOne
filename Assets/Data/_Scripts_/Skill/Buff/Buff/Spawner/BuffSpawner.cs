using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffSpawner : Spawner
{
    [SerializeField] private int buffIndex = 0;
    [SerializeField] private List<BuffObject> buffObjects;
    public event Action<int, BEntityStats, NonDamageType> OnHPBuff;
    public event Action<BuffObject> OnBuffObjectChange;
    public event Action<int> OnBuffObjectDespawn;

    public void SpawnBuffs(int level, List<ActiveBuff> buffs, BEntityStats stats, SkillActivator activator)
    {
        GetCurrentBuff();

        foreach (var buff in buffs)
        {
            SpawnBuff(level, buff, stats, activator);
        }
    }

    public void SpawnBuff(int level, ActiveBuff buff, BEntityStats stats, SkillActivator activator)
    {
        if (buff.DurationType == DurationType.Immediately)
        {
            ImmediatelyBuffHandling(level, buff, stats);
        }
        else if (buff.DurationType == DurationType.NextStrike)
        {
            Battle.Instance.PlayerNextDamage = DamageType.None;
            BuffObject foundObj = FindBuff(buff);

            if (foundObj == null)
            {
                SpawnNewBuff(level, buff, stats, activator);
            }
        }
        else
        {
            if (buff.DurationStack)
            {
                BuffObject foundObj = FindBuff(buff);

                if (foundObj != null)
                {
                    foundObj.Duration += buff.Duration;
                    OnBuffObjectChange?.Invoke(foundObj);
                }
                else
                {
                    SpawnNewBuff(level, buff, stats, activator);
                }
            }
            else if (buff.PercentStack)
            {
                BuffObject foundObj = FindBuff(buff);

                if (foundObj != null)
                {
                    float percentBuff = buff.PercentBonus * (level - 1) + buff.BuffPercent;
                    foundObj.PercentBuff += percentBuff;
                    foundObj.BuffHandler.BuffHandling();
                    OnBuffObjectChange?.Invoke(foundObj);
                }
                else
                {
                    SpawnNewBuff(level, buff, stats, activator);
                }
            }
            else
            {
                BuffObject foundObj = FindBuff(buff);

                if (foundObj != null) foundObj.Duration = buff.Duration;
                else SpawnNewBuff(level, buff, stats, activator);
            }
        }
    }

    public void SpawnNewBuff(int level, ActiveBuff buff, BEntityStats stats, SkillActivator activator)
    {
        buffIndex = buffIndex++ > 100 ? 0 : buffIndex++;

        Transform newBuff = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);

        BuffObject objCom = newBuff.GetComponent<BuffObject>();
        objCom.Index = buffIndex;
        objCom.Icon = buff.Icon;
        objCom.Stats = stats;
        objCom.SourceStat = buff.SourceStat;
        objCom.TrueStatBuff = buff.StatBuff;
        objCom.DamageType = buff.DamageType;
        float percentBuff = buff.PercentBonus * (level - 1) + buff.BuffPercent;
        objCom.PercentBuff = percentBuff;
        objCom.DurationType = buff.DurationType;
        objCom.Duration = buff.Duration;
        objCom.DurationStack = buff.DurationStack;
        objCom.PercentStack = buff.PercentStack;
        objCom.Description = buff.Description;
        objCom.Activator = activator;

        newBuff.gameObject.SetActive(true);
        OnBuffObjectChange?.Invoke(objCom);
    }

    public void ImmediatelyBuffHandling(int level, ActiveBuff buff, BEntityStats stats)
    {
        float percentBuff = buff.PercentBonus * (level - 1) + buff.BuffPercent;

        switch (buff.StatBuff)
        {
            case EquipStatType.CurrentHPByMaxHP:
                int insAmount = (int)(percentBuff / 100 * stats.MaxHP);

                stats.HPIns(insAmount);
                OnHPBuff?.Invoke(insAmount, stats, NonDamageType.HP);
                break;
        }
    }

    private BuffObject FindBuff(ActiveBuff buff)
    {
        foreach (var buffObj in buffObjects)
        {
            if (buffObj.TrueStatBuff == buff.StatBuff && buffObj.SourceStat == buff.SourceStat && buffObj.DurationType == buff.DurationType)
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

    public void CallOnBuffChangeEvent(BuffObject buff)
    {
        Debug.Log("call change");
        OnBuffObjectChange?.Invoke(buff);
    }

    public void CallOnBuffDespawnEvent(BuffObject buff)
    {
        Debug.Log("call despawn");
        OnBuffObjectDespawn?.Invoke(buff.Index);
    }
}