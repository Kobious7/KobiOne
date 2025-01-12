using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class SkillUI : GMono
    {
        [SerializeField] private Button btn;

        public Button Btn => btn;

        [SerializeField] private SkillUILock skillUILock;

        public SkillUILock SkillUILock => skillUILock;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
            LoadSkillLock();
        }

        private void LoadButton()
        {
            if (btn != null) return;

            btn = GetComponent<Button>();
        }

        private void LoadSkillLock()
        {
            if (skillUILock != null) return;

            skillUILock = GetComponentInChildren<SkillUILock>();
        }
    }
}