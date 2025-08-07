using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentInvUISpawner : Spawner
{
    [SerializeField] private EquipUI currentEquip;

    public EquipUI CurrentEquip
    {
        get => currentEquip;
        set => currentEquip = value;
    }

    [SerializeField] private List<EquipUI> currentList;

    public List<EquipUI> CurrentList
    {
        get => currentList;
        set => currentList = value;
    }

    protected override void LoadHolder()
    {
        if (holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnEquips(List<InventoryEquip> equipInv)
    {
        if (equipInv.Count <= 0) return;

        currentList = new();

        foreach (var equip in equipInv)
        {
            SpawnEquip(equip);
        }
    }

    public void SpawnEquip(InventoryEquip equip)
    {
        Transform newEquip = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
        newEquip.localScale = Vector3.one;
        EquipUI equipUI = newEquip.GetComponent<EquipUI>();

        currentList.Add(equipUI);
        equipUI.ShowEquip(equip);
        equipUI.Button.onClick.RemoveAllListeners();
        equipUI.Button.onClick.AddListener(() => AddClick(equipUI));
        newEquip.gameObject.SetActive(true);
    }

    public void AddClick(EquipUI equipUI)
    {
        if (currentEquip == equipUI) return;
        if (equipUI.Equip.IsNew)
        {
            equipUI.Equip.IsNew = false;
            equipUI.NewIcon.gameObject.SetActive(false);
            NewAndLockEquip.Instance.OnNewOrLockChangedEventInvoke(equipUI.Equip, true);
        }

        if (GameUI.Instance.CurrentEquipmentUI.CurrentEquip != null)
        {
            GameUI.Instance.CurrentEquipmentUI.CurrentEquip.OnSelected.gameObject.SetActive(false);
            GameUI.Instance.CurrentEquipmentUI.CurrentEquip = null;
        }

        if (currentEquip != null) currentEquip.OnSelected.gameObject.SetActive(false);

        equipUI.OnSelected.gameObject.SetActive(true);

        currentEquip = equipUI;

        EquipmentDetailsUI.Instance.gameObject.SetActive(true);
        EquipmentDetailsUI.Instance.ShowDetails(equipUI);
        EquipmentDetailsUI.Instance.AddEquipClickListener(equipUI.Equip);
    }

    public void DespawnEquip(InventoryEquip equip)
    {
        Transform despawnEquip = currentList.Where(e => e.Equip == equip).FirstOrDefault().transform;

        currentList.Remove(despawnEquip.GetComponent<EquipUI>());
        Despawn(despawnEquip);
    }
    
    public void GetEquipUIAndChangeNewLock(InventoryEquip equip)
    {
        if (currentList.Count <= 0) return;
        EquipUI equipUI = currentList.Where(e => e.Equip == equip).FirstOrDefault();

        equipUI.ChangeLock();
    }
}