using UnityEngine;
using UnityEngine.UI;

public class EquipmentUpgradeUICloseBtn : GMono
{
    [SerializeField] private Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        button = GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(Click);
    }

    private void Click()
    {
        transform.parent.gameObject.SetActive(false);
    }
}