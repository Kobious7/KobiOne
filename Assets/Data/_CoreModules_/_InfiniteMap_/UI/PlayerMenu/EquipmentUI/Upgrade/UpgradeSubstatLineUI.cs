using UnityEngine;

public class UpgradeSubstatLineUI : GMono
{
    [SerializeField] private Transform newStat;
    [SerializeField] private UpgradeStatLineUI statUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        newStat = transform.Find("NewStat");
        statUI = GetComponentInChildren<UpgradeStatLineUI>();

        newStat.gameObject.SetActive(false);
    }

    public void SetSubstatLine(EquipStat stat, EquipStatBonus defaultRarityBonus)
    {
        if (stat == null)
        {
            newStat.gameObject.SetActive(true);
            statUI.gameObject.SetActive(false);
        }
        else
        {
            newStat.gameObject.SetActive(false);
            statUI.gameObject.SetActive(true);
            statUI.SetSubstatLine(stat, defaultRarityBonus);
        }
    }
}