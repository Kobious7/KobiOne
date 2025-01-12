using System.Collections.Generic;
using UnityEngine;

namespace InfiniteMap
{
    public class CurrentSkill : GMono
    {
        [SerializeField] private SkillUI skillUI;

        public SkillUI SkillUI => skillUI;

        [SerializeField] private Transform tempSkill;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSkillUI();
        }

        private void LoadSkillUI()
        {
            if(skillUI != null) return;

            skillUI = GetComponent<SkillUI>();
        }

        public void ChangeSkill(int index)
        {
            if(skillUI.SkillSO == null)
            {
                RemoveSkill(index, SkillUISpawner.Instance.SkillList);
            }
            else
            {
                var tempSprite = skillUI.Model.sprite;
                var tempSkillSO = skillUI.SkillSO;
                SkillUI skill = SkillUISpawner.Instance.GetSkillUI(index);
                skillUI.Model.sprite = skill.Model.sprite;
                skillUI.SkillSO = skill.SkillSO;
                skill.Model.sprite = tempSprite;
                skill.SkillSO = tempSkillSO;
            }
        }

        public void RemoveSkill(int index, List<SkillUI> skillList)
        {
            while (index != skillList.Count - 1)
            {
                var tempSkillSO = skillList[index].SkillSO;

                skillList[index].SkillSO = skillList[index + 1].SkillSO;
                skillList[index + 1].SkillSO = tempSkillSO;
                skillList[index].ShowSkillUI();
                skillList[index + 1].ShowSkillUI();
                index++;
            }

            tempSkill = skillList[index].transform;
            SkillUI skill = tempSkill.GetComponent<SkillUI>();
            skillUI.Model.sprite = skill.Model.sprite;
            skillUI.SkillSO = skill.SkillSO;

            SkillUISpawner.Instance.Despawn(skillList[index].transform);
            skillList.RemoveAt(index);
        }
    }
}