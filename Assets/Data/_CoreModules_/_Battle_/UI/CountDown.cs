using TMPro;
using UnityEngine;

public class CountDown : GMono
{
    private static CountDown instance;

    public static CountDown Instance => instance;

    [SerializeField] private TextMeshProUGUI textM;

    public TextMeshProUGUI TextM
    {
        get { return textM; }
        set { textM = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
    }

    private void LoadText()
    {
        if (textM != null) return;

        textM = GetComponent<TextMeshProUGUI>();
    }
}