using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSubStatsSpawner : Spawner
{
    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnSubStats(List<EquipStat> subStats)
    {
        DespawnAllStatUI();
        
        if(subStats.Count <= 0) return;

        foreach(var subStat in subStats)
        {
            Transform newSubStat = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newSubStat.localScale = Vector3.one;
            EquipmentStatUI equipmentStatUI = newSubStat.GetComponent<EquipmentStatUI>();

            equipmentStatUI.Show(subStat);
            newSubStat.gameObject.SetActive(true);
        }
    }

    private List<EquipmentStatUI> GetCurrentEquipmentStatUI()
    {
        return GetComponentsInChildren<EquipmentStatUI>().ToList();
    }

    private void DespawnAllStatUI()
    {
        List<EquipmentStatUI> equipmentStatUIList = GetCurrentEquipmentStatUI();

        if(equipmentStatUIList.Count <= 0) return;

        foreach(var equipmentStatUI in equipmentStatUIList)
        {
            Despawn(equipmentStatUI.transform);
        }
    }
}