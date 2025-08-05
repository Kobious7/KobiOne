using UnityEngine;

public class InvSpecialUI : InvEquipUI
{
    protected override void OffSelectedWithSecificEquip()
    {
        if(InvSpecialUISpawner.Instance.SelectedItem != null) InvSpecialUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvSpecialUISpawner.Instance.SelectedItem = onSelectObject;
    }
}