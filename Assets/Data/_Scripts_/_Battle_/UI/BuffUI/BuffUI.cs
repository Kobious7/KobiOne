using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : GMono
{
    [SerializeField] private int index;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private Transform hint;
    [SerializeField] private TextMeshProUGUI hintText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadImage();
        LoadButton();
        LoadDurationText();
        LoadHintAndHintText();
    }

    private void LoadImage()
    {
        if (image != null) return;

        image = transform.Find("Image").GetComponent<Image>();
    }

    private void LoadButton()
    {
        if (button != null) return;

        button = GetComponent<Button>();
    }

    private void LoadDurationText()
    {
        if (durationText != null) return;

        durationText = transform.Find("DurationText").GetComponent<TextMeshProUGUI>();
    }

    private void LoadHintAndHintText()
    {
        if (hint != null && hintText != null) return;

        hint = transform.Find("Hint");
        hintText = hint.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetBuffUI(BuffInfo buff)
    {
        index = buff.Index;
        image.sprite = buff.Icon;
        durationText.text = $"{buff.Duration}";
        string description = buff.Description;
        string number = "";

        if (buff.DurationType == DurationType.NextStrike)
        {
            durationText.text = "";
        }

        if (buff.DurationType == DurationType.TURN)
        {
            number = buff.Duration > 1 ? "turns" : "turn";
        }

        if (buff.DurationType == DurationType.CYCLE)
        {
            number = buff.Duration > 1 ? "cycles" : "cycle";
        }
        
        description = description.Replace("{Value}", $"{buff.PercentBuff}");
        description = description.Replace("{StatBuff}", $"{buff.TrueStatBuff}");
        description = description.Replace("{Duration}", $"{buff.Duration} {number}");
        hintText.text = description;

        hint.gameObject.SetActive(false);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Click);
    }

    public void UpdateBuffUI(BuffInfo buff)
    {
        durationText.text = $"{buff.Duration}";
        string description = buff is BuffObject ? "+{Value}% {StatBuff} in {Duration}" : "{Value}% {StatBuff} in {Duration}";
        string number = "";

        if (buff.DurationType == DurationType.NextStrike)
        {
            durationText.text = "";
        }

        if (buff.DurationType == DurationType.TURN)
        {
            number = buff.Duration > 1 ? "turns" : "turn";
        }

        if (buff.DurationType == DurationType.CYCLE)
        {
            number = buff.Duration > 1 ? "cycles" : "cycle";
        }
        
        description = description.Replace("{Value}", $"{buff.PercentBuff}");
        description = description.Replace("{StatBuff}", $"{buff.TrueStatBuff}");
        description = description.Replace("{Duration}", $"{buff.Duration} {number}");
        hintText.text = description;
    }

    private void Click()
    {
        if (hint.gameObject.activeSelf)
        {
            hint.gameObject.SetActive(false);
        }
        else
        {
            hint.gameObject.SetActive(true);
        }
    }
}