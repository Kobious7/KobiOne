using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class EquipmentOpenBtnUIClick : GMono
    {
        [SerializeField] private Button btn;

        [SerializeField] private Transform potential;
        [SerializeField] private Transform statSpawner;
        [SerializeField] private Transform equipment;
        [SerializeField] private Transform preview;
        [SerializeField] private Transform skill;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
            LoadPotential();
            LoadStatSpawner();
            LoadEquipment();
            LoadPreview();
            LoadSkill();
        }

        protected override void Start()
        {
            base.Start();
            btn.onClick.AddListener(OnClick);
        }

        private void LoadButton()
        {
            if(btn != null) return;

            btn = GetComponent<Button>();
        }

        private void LoadPotential()
        {
            if(potential != null) return;

            potential = transform.parent.Find("Potential");
        }

        private void LoadStatSpawner()
        {
            if(statSpawner != null) return;

            statSpawner = transform.parent.Find("LineStatSpawner");
        }

        private void LoadEquipment()
        {
            if(equipment != null) return;

            equipment = transform.parent.Find("Equipment");
        }

        private void LoadPreview()
        {
            if(preview != null) return;

            preview = transform.parent.Find("Preview");
        }

        private void LoadSkill()
        {
            if(skill != null) return;

            skill = transform.parent.Find("Skill");
        }

        private void OnClick()
        {
            if(potential.gameObject.activeSelf) potential.gameObject.SetActive(false);
            if(statSpawner.gameObject.activeSelf) statSpawner.gameObject.SetActive(false);
            if(skill.gameObject.activeSelf) skill.gameObject.SetActive(false);
            if(!preview.gameObject.activeSelf) preview.gameObject.SetActive(true);
            if(!equipment.gameObject.activeSelf) equipment.gameObject.SetActive(true);
        }
    }
}