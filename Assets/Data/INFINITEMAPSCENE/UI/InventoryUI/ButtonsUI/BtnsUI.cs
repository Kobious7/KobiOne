using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnsUI : GMono
{
    [SerializeField] private Button useBtn;

    public Button UseButton => useBtn;

    [SerializeField] private Button removeBtn;

    public Button RemoveButton => removeBtn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButtons();
    }

    private void LoadButtons()
    {
        if (useBtn != null && removeBtn != null) return;

        useBtn = transform.Find("UseBtn").GetComponent<Button>();
        removeBtn = transform.Find("RemoveBtn").GetComponent<Button>();
    }

    public void UseBtnAddListener(InventoryItem item)
    {
        useBtn.onClick.AddListener(() => OnUseBtnClick(item));
    }

    private void OnUseBtnClick(InventoryItem item)
    {
        Debug.Log("Use " + item.ItemSO.name);
    }

    public void RemoveBtnAddListener(InventoryItem item)
    {
        removeBtn.onClick.AddListener(() => OnRemoveBtnClick(item));
    }

    private void OnRemoveBtnClick(InventoryItem item)
    {
        Debug.Log("Remove " + item.ItemSO.name);
    }
}