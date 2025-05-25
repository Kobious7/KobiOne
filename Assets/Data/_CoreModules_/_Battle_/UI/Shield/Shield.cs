using TMPro;
using UnityEngine;

public class Shield : GMono
{
    [SerializeField] private TextMeshProUGUI stackText;

    public TextMeshProUGUI StackText => stackText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadStackText();
    }

    private void LoadStackText()
    {
        if (stackText != null) return;

        stackText = GetComponentInChildren<TextMeshProUGUI>();
    }
}