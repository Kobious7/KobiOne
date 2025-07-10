using UnityEngine;
using UnityEngine.UI;

public class DisableRewardItemBtn : GMono
{
    private Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        button = GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
    }
}