using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : GMono
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<Button> copyButtons;
    [SerializeField] private Button arrowRight;
    [SerializeField] private Button arrowLeft;
    [SerializeField] private int currentIndex;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadArrowButtons();

        if (buttons.Count <= 0) buttons = GetComponentsInChildren<Button>().ToList();
    }

    protected override void Start()
    {
        base.Start();

        SetButtons();
    }

    public void SetButtons()
    {
        copyButtons = new(buttons);

        if (!SavingManager.Instance.IsDataExist)
        {
            copyButtons[0].gameObject.SetActive(false);
            copyButtons.RemoveAt(0);
        }

        for (int i = 0; i < copyButtons.Count; i++)
        {
            if (i == 0) continue;
            copyButtons[i].gameObject.SetActive(false);
        }

        currentIndex = 0;

        if (copyButtons.Count > 1)
        {
            arrowRight.onClick.AddListener(() => RightClick(currentIndex));
            arrowLeft.onClick.AddListener(() => LeftClick(currentIndex));
        }
        else
        {
            arrowLeft.onClick.RemoveAllListeners();
            arrowRight.onClick.RemoveAllListeners();
        }
    }

    private void LoadArrowButtons()
    {
        if (arrowRight != null && arrowLeft != null) return;

        arrowRight = transform.parent.Find("ArrowRight").GetComponent<Button>();
        arrowLeft = transform.parent.Find("ArrowLeft").GetComponent<Button>();
    }

    private void RightClick(int index)
    {
        copyButtons[index].gameObject.SetActive(false);

        int newIndex = index + 1 >= buttons.Count ? 0 : index + 1;

        copyButtons[newIndex].gameObject.SetActive(true);

        currentIndex = newIndex;
    }

    private void LeftClick(int index)
    {
        copyButtons[index].gameObject.SetActive(false);

        int newIndex = index - 1 < 0 ? buttons.Count - 1 : index - 1;

        copyButtons[newIndex].gameObject.SetActive(true);

        currentIndex = newIndex;
    }
}
