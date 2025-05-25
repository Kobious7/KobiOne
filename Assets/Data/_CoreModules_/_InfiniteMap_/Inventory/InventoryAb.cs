using UnityEngine;

public abstract class InventoryAb : GMono
{
    [SerializeField] private Inventory inventory;

    public Inventory Inventory => inventory;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInventory();
    }

    private void LoadInventory()
    {
        if(inventory != null) return;

        inventory = transform.parent.GetComponent<Inventory>();
    }
}
