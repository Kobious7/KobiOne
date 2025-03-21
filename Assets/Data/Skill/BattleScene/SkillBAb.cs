using UnityEngine;

namespace Battle
{
    public abstract class SkillBAb : GMono
    {
        [SerializeField] private SkillB skillB;

        public SkillB SkillB => skillB;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSkillB();
        }

        private void LoadSkillB()
        {
            if(skillB != null) return;

            skillB = transform.parent.GetComponent<SkillB>();
        }
    }
}