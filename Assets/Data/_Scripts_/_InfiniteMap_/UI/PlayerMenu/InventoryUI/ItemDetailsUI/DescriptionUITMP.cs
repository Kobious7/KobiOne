using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUITMP : GMono
{
    [SerializeField] private TextMeshProUGUI description;

    public TextMeshProUGUI Description => description;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDescription();
    }

    private void LoadDescription()
    {
        if(description != null) return;

        description = GetComponentInChildren<TextMeshProUGUI>(); 
    }
}