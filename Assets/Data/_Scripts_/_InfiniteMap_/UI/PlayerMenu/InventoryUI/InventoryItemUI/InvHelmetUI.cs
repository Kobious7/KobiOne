using UnityEngine;

public class InvHelmetUI : InvEquipUI
{
    protected override void OffSelectedWithSecificEquip()
    {
        if(InvHelmetUISpawner.Instance.SelectedItem != null) InvHelmetUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvHelmetUISpawner.Instance.SelectedItem = onSelectObject;
    }
}