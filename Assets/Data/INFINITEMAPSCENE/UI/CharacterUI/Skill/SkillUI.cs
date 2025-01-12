using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class SkillUI : GMono
    {
        [SerializeField] private int skillIndex;

        public int SkillIndex
        {
            get => skillIndex;
            set => skillIndex = value;
        }

        [SerializeField] private SkillSO skillSO;

        public SkillSO SkillSO
        {
            get => skillSO;
            set => skillSO = value;
        }

        [SerializeField] private Image model;

        public Image Model
        {
            get => model;
            set => model = value;
        }

        [SerializeField] private TextMeshProUGUI skillButton;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadImage();
            LoadSkillButton();
        }

        private void LoadImage()
        {
            if(model != null) return;

            model = transform.Find("Model").GetComponent<Image>();
        }

        private void LoadSkillButton()
        {
            if(skillButton != null) return;

            skillButton = GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowSkillUI()
        {
            model.sprite = skillSO.SkillIcon;
            skillButton.text = skillSO.Button == SkillButton.Q ? "Q" :
                                skillSO.Button == SkillButton.E ? "E" :
                                skillSO.Button == SkillButton.SPACE ? "Space" : "";
        }
    }
}