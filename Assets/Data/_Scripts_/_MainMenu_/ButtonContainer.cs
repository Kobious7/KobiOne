using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : GMono
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Button arrowRight;
    [SerializeField] private Button arrowLeft;
    [SerializeField] private int currentIndex;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadArrowButtons();
    }

    protected override void Start()
    {
        base.Start();

        buttons = GetComponentsInChildren<Button>().ToList();

        if (!SavingManager.Instance.IsDataExist)
        {
            buttons[0].gameObject.SetActive(false);
            buttons.RemoveAt(0);
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == 0) continue;
            buttons[i].gameObject.SetActive(false);
        }

        currentIndex = 0;

        arrowRight.onClick.AddListener(() => RightClick(currentIndex));
        arrowLeft.onClick.AddListener(() => LeftClick(currentIndex));
    }

    private void LoadArrowButtons()
    {
        if(arrowRight != null && arrowLeft != null) return;

        arrowRight = transform.parent.Find("ArrowRight").GetComponent<Button>();
        arrowLeft = transform.parent.Find("ArrowLeft").GetComponent<Button>();
    }

    private void RightClick(int index)
    {
        buttons[index].gameObject.SetActive(false);

        int newIndex = index + 1 >= buttons.Count ? 0 : index + 1;

        buttons[newIndex].gameObject.SetActive(true);

        currentIndex = newIndex;
    }

    private void LeftClick(int index)
    {
        buttons[index].gameObject.SetActive(false);

        int newIndex = index - 1 < 0 ? buttons.Count - 1 : index - 1;

        buttons[newIndex].gameObject.SetActive(true);

        currentIndex = newIndex;
    }
}
