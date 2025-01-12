using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Skill : GMono
    {
        [SerializeField] private SkillProperties properties;

        public SkillProperties Properties => properties;

        [SerializeField] private SkillFindTarget findTarget;

        public SkillFindTarget FindTarget => findTarget;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadProperties();
            LoadTargets();
        }

        private void LoadProperties()
        {
            if (properties != null) return;

            properties = GetComponentInChildren<SkillProperties>();
        }

        private void LoadTargets()
        {
            if (findTarget != null) return;

            findTarget = GetComponentInChildren<SkillFindTarget>();
        }
    }
}