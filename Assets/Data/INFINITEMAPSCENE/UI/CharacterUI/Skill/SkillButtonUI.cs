using System;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class SkillButtonUI : GMono
    {
        [SerializeField] private Button button;
        [SerializeField] private Transform onActive;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
            LoadOnActive();
        }

        protected override void Start()
        {
            base.Start();
            button.onClick.AddListener(Click);
        }

        private void LoadButton()
        {
            if(button != null) return;

            button = GetComponent<Button>();
        }

        private void LoadOnActive()
        {
            if(onActive != null) return;

            onActive = transform.parent.Find("OnClick");
        }

        private void Click()
        {
            if(onActive.gameObject.activeSelf) return;

            if(SkillUISpawner.Instance.CurrentActive != null) SkillUISpawner.Instance.CurrentActive.gameObject.SetActive(false);
            onActive.gameObject.SetActive(true);
            
            SkillUI skillUI = transform.parent.GetComponent<SkillUI>();
            SkillUISpawner.Instance.CurrentActive = onActive;

            UseSkillBtn.Instance.AddListenerClick(skillUI.SkillIndex);
            SkillDescription.Instance.DescritionTMP.text = "" + skillUI.SkillSO.Description;
        }
    }
}