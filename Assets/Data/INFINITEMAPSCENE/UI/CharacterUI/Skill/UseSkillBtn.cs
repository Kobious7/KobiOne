using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class UseSkillBtn : GMono
    {
        private static UseSkillBtn instance;

        public static UseSkillBtn Instance => instance;

        [SerializeField] private Button button;

        private SkillUISpawner skillUISpawner;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.LogError("Only 1 UseSkillBtn is allowed to exist!");

            instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
        }

        private void LoadButton()
        {
            if(button != null) return;

            button = GetComponent<Button>();
        }

        public void AddListenerClick(int index)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => UseSkill(index));
        }

        private void UseSkill(int index)
        {
            List<SkillUI> skillList = SkillUISpawner.Instance.SkillList;

            if (skillList[index].SkillSO.Button == SkillButton.Q)
            {
                CurrentQSkill.Instance.ChangeSkill(index);
                return;
            }

            if (skillList[index].SkillSO.Button == SkillButton.E)
            {
                return;
            }

            if (skillList[index].SkillSO.Button == SkillButton.SPACE)
            {
                return;
            }
        }
    }
}