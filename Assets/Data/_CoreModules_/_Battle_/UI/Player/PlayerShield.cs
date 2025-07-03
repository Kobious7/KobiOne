using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield : Shield
{
    [SerializeField] private Button button;

    public Button Button => button;

    [SerializeField] private Transform hint;

    public Transform Hint => hint;

    protected override void LoadComponents()
    {
        base.LoadComponents();;
        LoadButton();
        LoadHint();
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(Click);
    }

    private void LoadButton()
    {
        if (button != null) return;

        button = GetComponent<Button>();
    }

    private void LoadHint()
    {
        if (hint != null) return;

        hint = transform.Find("Hint");
        
        hint.gameObject.SetActive(false);
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