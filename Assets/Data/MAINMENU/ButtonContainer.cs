using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class ButtonContainer : GMono
    {
        [SerializeField] private List<Button> buttons;
        [SerializeField] private Button arrowRight;
        [SerializeField] private Button arrowLeft;
        [SerializeField] private int currentIndex;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButtons();
            LoadArrowButtons();
        }

        protected override void Start()
        {
            base.Start();

            for(int i = 0; i < buttons.Count; i++)
            {
                if(i > 0) buttons[i].gameObject.SetActive(false);
            }

            currentIndex = 0;

            arrowRight.onClick.AddListener(() => RightClick(currentIndex));
            arrowLeft.onClick.AddListener(() => LeftClick(currentIndex));
        }

        private void LoadButtons()
        {
            if(buttons.Count > 0) return;

            buttons = GetComponentsInChildren<Button>().ToList();
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
}