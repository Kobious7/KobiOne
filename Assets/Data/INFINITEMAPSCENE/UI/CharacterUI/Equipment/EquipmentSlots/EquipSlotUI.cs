using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class EquipSlotUI : GMono
    {
        [SerializeField] private Transform onSelected;

        public Transform OnSelected => onSelected;

        [SerializeField] private Image model;

        public Image Model => model;

        [SerializeField] private Button btn;

        public Button Btn => btn;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadOnSelected();
            LoadModel();
            LoadButton();
        }

        private void LoadOnSelected()
        {
            if(onSelected != null) return;

            onSelected = transform.Find("OnSelected");
        }

        private void LoadModel()
        {
            if(model != null) return;

            model = transform.Find("Model").GetComponent<Image>();
        }

        private void LoadButton()
        {
            if(btn != null) return;

            btn = GetComponentInChildren<Button>();
        }
    }
}