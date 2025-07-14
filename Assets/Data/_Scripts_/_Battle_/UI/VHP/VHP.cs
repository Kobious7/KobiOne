using UnityEngine;
using UnityEngine.UI;

public class VHP : GMono
{
    [SerializeField] private Image model;

    public Image Model => model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
    }

    private void LoadModel()
    {
        if (model != null) return;

        model = transform.Find("Model").GetComponent<Image>();
    }
}