using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class EquipUI : GMono
    {
        private InventoryEquip equip;
        
        public InventoryEquip Equip
        {
            get => equip;
            set => equip = value;
        }

        [SerializeField] private Transform onSelected;
        
        public Transform OnSelected => onSelected;

        [SerializeField] private Image qualityColor;

        public Image QualityColor => qualityColor;

        [SerializeField] private Image model;

        public Image Model => model;

        [SerializeField] private Button button;

        public Button Button => button;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadOnSelected();
            LoadQualityColor();
            LoadModel();
            LoadButton();
        }

        private void LoadOnSelected()
        {
            if(onSelected != null) return;

            onSelected = transform.Find("OnSelected");
        }

        private void LoadQualityColor()
        {
            if(qualityColor != null) return;

            qualityColor = transform.Find("BG").GetComponent<Image>();
        }

        private void LoadModel()
        {
            if(model != null) return;

            model = transform.Find("Model").GetComponent<Image>();
        }

        private void LoadButton()
        {
            if(button != null) return;

            button = GetComponent<Button>();
        }

        public void ShowEquip(InventoryEquip equip)
        {
            this.equip = equip;
            qualityColor.color = GetQualityColorByRarity(equip.Rarity);
            model.sprite = equip.EquipSO.Sprite;
        }

        public void ShowEmpty()
        {
            equip = new InventoryEquip();
            qualityColor.color = Color.white;
            model.sprite = null;
        }
    }
}