using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStatsVHPPlayer : GMono
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button ins;
    [SerializeField] private Button des;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAll();
    }

    private void LoadAll()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        ins = transform.Find("Increase").GetComponent<Button>();
        des = transform.Find("Decrease").GetComponentInChildren<Button>();
    }

    protected override void Start()
    {
        base.Start();
        ins.onClick.AddListener(Increase);
        des.onClick.AddListener(Decrease);
    }

    private void Increase()
    {
        BattleManager.Instance.Player.Stats.VHPIns(int.Parse(inputField.text));
    }

    private void Decrease()
    {
        BattleManager.Instance.Player.Stats.VHPDes(int.Parse(inputField.text));
    }
}