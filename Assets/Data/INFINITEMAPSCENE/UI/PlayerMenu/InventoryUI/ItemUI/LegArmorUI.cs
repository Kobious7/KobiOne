using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class LegArmorUI : GMono
    {
        [SerializeField] private int index;

        public int Index
        {
            get => index;
            set => index = value;
        }

        [SerializeField] private EquipSO equipSO;

        public EquipSO EquipSO
        {
            get => equipSO;
            set => equipSO = value;
        }

        [SerializeField] private Image model;

        public Image Model
        {
            get => model;
            set => model = value;
        }

        [SerializeField] private Button btn;

        public Button Btn
        {
            get => btn;
            set => btn = value;
        }

        [SerializeField] private Transform onSelectObject;
        [SerializeField] private float lastTimeClick, doubleClickTheshold = 0.2f;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModel();
            LoadButton();
            LoadOnSelectObject();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            btn.onClick.AddListener(Click);
        }

        private void LoadModel()
        {
            if(model != null) return;

            model = transform.Find("Image").GetComponent<Image>();
        }
        
        private void LoadButton()
        {
            if(btn != null) return;

            btn = GetComponentInChildren<Button>();
        }

        private void LoadOnSelectObject()
        {
            if(onSelectObject != null) return;

            onSelectObject = transform.Find("OnSelect");
        }

        public void ShowLegArmor(InventoryEquip item)
        {
            model.sprite = item.EquipSO.Sprite;
        }

        private void Click()
        {
            if(!onSelectObject.gameObject.activeSelf)
            {
                if(Time.time - lastTimeClick < doubleClickTheshold)
                {
                    EquipDetailsUI.Instance.gameObject.SetActive(true);
                    EquipDetailsUI.Instance.ShowEquip(Game.Instance.Inventory.LegArmorList[index]);
                }

                lastTimeClick = Time.time;

                return;
            }

            if(LegArmorUISpawner.Instance.SelectedItem != null) LegArmorUISpawner.Instance.SelectedItem.gameObject.SetActive(true);
            onSelectObject.gameObject.SetActive(false);

            LegArmorUISpawner.Instance.SelectedItem = onSelectObject;
        }
    }
}