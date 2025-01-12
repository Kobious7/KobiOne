using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class ButtonClick : GMono
    {
        [SerializeField] protected Button button;
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
        }

        protected override void Start()
        {
            base.Start();
            button.onClick.AddListener(Click);
        }

        private void LoadButton()
        {
            if(button != null) return;

            button = GetComponent<Button>();
        }

        protected virtual void Click()
        {
            //Override
        }
    }
}