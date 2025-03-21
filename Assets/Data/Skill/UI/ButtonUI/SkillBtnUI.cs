using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class SkillBtnUI : GMono
    {
        protected DestructiveObjectSpawner destructiveObjectSpawner;

        [SerializeField] protected Button button;

        public Button Button => button;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
        }

        protected override void Start()
        {
            base.Start();
            destructiveObjectSpawner = DestructiveObjectSpawner.Instance;

            button.onClick.AddListener(OnButtonClick);
        }

        private void FixedUpdate()
        {
            UpdateButtonUnlocking();
        }

        private void LoadButton()
        {
            if(button != null) return;

            button = GetComponent<Button>();
        }

        private void OnButtonClick()
        {
            Click();
        }

        protected virtual void Click()
        {
            //Override
        }

        private void UpdateButtonUnlocking()
        {
            if(!GetManaCost())
            {
                button.interactable = false;
                ColorBlock colorBlock = button.colors;
                colorBlock.normalColor = Color.black;
                button.colors = colorBlock;
            }
            else
            {
                button.interactable = true;
                ColorBlock colorBlock = button.colors;
                colorBlock.normalColor = Color.white;
                button.colors = colorBlock;
            }
        }

        protected virtual bool GetManaCost()
        {
            return false;
        }
    }
}