using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotentialStatUI : GMono
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI point;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
        LoadPoint();
    }

    private void LoadButton()
    {
        if(button != null) return;

        button = GetComponent<Button>();
    }

    private void LoadPoint()
    {
        if(point != null) return;

        point = transform.Find("Point").GetComponent<TextMeshProUGUI>();
    }

    public void ShowPoint(Stat potential)
    {
        point.text = potential.Value + "";
    }

    public void AddClickListener(Stat potential)
    {
        button.onClick.AddListener(() => Click(potential));
    }

    private void Click(Stat potential)
    {
        PotentialUpgradeUI.Instance.gameObject.SetActive(true);
        PotentialUpgradeUI.Instance.AddClickListener(potential.Index);
    }
}