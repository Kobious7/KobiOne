using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSetUI : GMono
{
    [SerializeField] private Button button, okBtn;
    [SerializeField] private TMP_InputField inputField;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        button = GetComponent<Button>();
        okBtn = transform.Find("OKBtn").GetComponent<Button>();
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    protected override void Start()
    {
        base.Start();

        button.onClick.AddListener(CloseClickListener);
        okBtn.onClick.AddListener(NameConfirmClickListener);

        this.gameObject.SetActive(false);
    }

    private void CloseClickListener()
    {
        this.gameObject.SetActive(false);
    }

    private void NameConfirmClickListener()
    {
        SavingManager.Instance.PlayerSO.PlayerName = inputField.text;
        LoadScene(INFINITEMAP);
    }
}