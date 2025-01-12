using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class EquipmentItemUI : GMono
    {
        [SerializeField] private Transform onSelected;

        public Transform OnSelected => onSelected;

        [SerializeField] private Image model;

        public Image Model => model;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadOnSelected();
            LoadModel();
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
    }
}