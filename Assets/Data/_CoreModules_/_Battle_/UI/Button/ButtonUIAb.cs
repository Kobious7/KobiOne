using UnityEngine;

public abstract class ButtonUIBAb : GMono
{
    [SerializeField] private ButtonUIB buttonUI;

    public ButtonUIB ButtonUI => buttonUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButtonUI();
    }

    private void LoadButtonUI()
    {
        if (buttonUI != null) return;
        
        buttonUI = transform.parent.GetComponent<ButtonUIB>();
    }
}