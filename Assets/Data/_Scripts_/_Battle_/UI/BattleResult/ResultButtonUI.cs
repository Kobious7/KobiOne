using UnityEngine;
using UnityEngine.UI;

public class ResultButtonUI : GMono
{
    [SerializeField] private Button btn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        btn = GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        btn.onClick.AddListener(LoadMapListener);
    }

    private void LoadMapListener()
    {
        LoadScene(INFINITEMAP);
    }
}