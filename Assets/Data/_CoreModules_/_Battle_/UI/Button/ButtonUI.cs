using UnityEngine;
using UnityEngine.UI;

public class ButtonUIB : GMono
{
    [SerializeField] private Button btn;

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