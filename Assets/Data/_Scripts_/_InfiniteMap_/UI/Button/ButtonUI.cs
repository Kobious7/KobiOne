using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : GMono
{
    [SerializeField] protected Button btn;

    public Button Btn => btn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    private void LoadButton()
    {
        if (btn != null) return;

        btn = GetComponent<Button>();
    }
}