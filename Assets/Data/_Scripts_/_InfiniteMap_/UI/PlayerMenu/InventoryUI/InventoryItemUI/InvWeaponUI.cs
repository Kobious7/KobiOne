using UnityEngine;

public class InvWeaponUI : InvEquipUI
{
    protected override void OffSelectedWithSecificEquip()
    {
        if(InvWeaponUISpawner.Instance.SelectedItem != null) InvWeaponUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvWeaponUISpawner.Instance.SelectedItem = onSelectObject;
    }
}