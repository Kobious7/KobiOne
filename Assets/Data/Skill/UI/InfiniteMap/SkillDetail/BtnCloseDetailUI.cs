using UnityEngine;
using UnityEngine.UI;

public class BtnCloseDetailUI : GMono
{
    [SerializeField] private Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(Click);
    }

    private void LoadButton()
    {
        if(button != null) return;

        button = GetComponent<Button>();
    }

    private void Click()
    {
        transform.parent.gameObject.SetActive(false);
    }
}