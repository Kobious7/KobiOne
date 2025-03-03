using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class CloseSpawnerAndDetailsUI : GMono
    {
        [SerializeField] private Button button;

        private CurrentEquipmentUI currentEquipmentUI;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
        }

        protected override void Start()
        {
            base.Start();
            currentEquipmentUI = GameUI.Instance.CurrentEquipmentUI;

            button.onClick.AddListener(Click);
        }

        private void LoadButton()
        {
            if(button != null) return;

            button = GetComponent<Button>();
        }

        private void Click()
        {
            if(currentEquipmentUI.CurrentEquip != null)
            {
                currentEquipmentUI.CurrentEquip.OnSelected.gameObject.SetActive(false);
                currentEquipmentUI.CurrentEquip = null;
            }

            if(currentEquipmentUI.CurrentSpawner != null)
            {
                if(currentEquipmentUI.CurrentSpawner.CurrentEquip != null)
                {
                    currentEquipmentUI.CurrentSpawner.CurrentEquip.OnSelected.gameObject.SetActive(false);
                    currentEquipmentUI.CurrentSpawner.CurrentEquip = null;
                }

                currentEquipmentUI.CurrentSpawner.gameObject.SetActive(false);
                currentEquipmentUI.CurrentSpawner = null;
            }

            EquipmentDetailsUI.Instance.gameObject.SetActive(false);
            transform.gameObject.SetActive(false);
        }
    }
}