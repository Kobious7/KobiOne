using UnityEngine;

public class InvArmwearUI : InvEquipUI
{
    protected override void OffSelectedWithSecificEquip()
    {
        if(InvArmwearUISpawner.Instance.SelectedItem != null) InvArmwearUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvArmwearUISpawner.Instance.SelectedItem = onSelectObject;
    }
}