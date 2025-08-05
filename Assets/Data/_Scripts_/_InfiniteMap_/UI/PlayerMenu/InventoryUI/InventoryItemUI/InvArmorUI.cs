using UnityEngine;

public class InvArmorUI : InvEquipUI
{
    protected override void OffSelectedWithSecificEquip()
    {
        if(InvArmorUISpawner.Instance.SelectedItem != null) InvArmorUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvArmorUISpawner.Instance.SelectedItem = onSelectObject;
    }
}