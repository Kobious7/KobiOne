using UnityEngine;

public class InvBootsUI : InvEquipUI
{
    protected override void OffSelectedWithSecificEquip()
    {
        if(InvBootsUISpawner.Instance.SelectedItem != null) InvBootsUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
        InvBootsUISpawner.Instance.SelectedItem = onSelectObject;
    }
}