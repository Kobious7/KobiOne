using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotentialUpgradeUI : GMono
{
    private static PotentialUpgradeUI instance;

    public static PotentialUpgradeUI Instance => instance;

    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private Button button;
    private int point;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 PotientialUIUpgrade is allow to exist!");

        instance = this;
        transform.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextInput();
        LoadButton();
    }

    protected override void Start()
    {
        base.Start();
        textInput.onValueChanged.AddListener(OnTextChange);
    }

    private void LoadTextInput()
    {
        if(textInput != null) return;

        textInput = transform.Find("Details").Find("InputField").GetComponent<TMP_InputField>();
    }

    private void LoadButton()
    {
        if(button != null) return;

        button = transform.Find("Details").GetComponentInChildren<Button>();
    }

    private void OnTextChange(string text)
    {
        point = string.IsNullOrWhiteSpace(text) ? 0 : int.Parse(text);
        point = InfiniteMapManager.Instance.Player.StatsSystem.CheckRemainPoints(point);
        string sPoint = point <= 0 ? "" : point + "";
        textInput.text = sPoint;
    }

    public void AddClickListener(int potential)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Click(potential));
    }

    private void Click(int potential)
    {
        if(point <= 0)
        {
            transform.gameObject.SetActive(false);
            return;
        }

        InfiniteMapManager.Instance.Player.StatsSystem.IncreasePotentialPoint(potential, point);

        point = 0;
        textInput.text = "";

        transform.gameObject.SetActive(false);
    }
}