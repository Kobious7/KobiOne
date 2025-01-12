using UnityEngine;

namespace Battle
{
    public abstract class SkillUIAb : GMono
    {
        [SerializeField] private SkillUI skill;

        public SkillUI Skill => skill;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSkill();
        }

        private void LoadSkill()
        {
            if (skill != null) return;

            skill = transform.parent.GetComponent<SkillUI>();
        }
    }
}