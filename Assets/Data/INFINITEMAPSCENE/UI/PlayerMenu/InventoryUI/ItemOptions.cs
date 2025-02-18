using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class ItemOptions : GMono
    { 
        [SerializeField] private List<RectTransform> options;
        [SerializeField] private List<Button> buttons;
        [SerializeField] private List<Spawner> spawners;
        [SerializeField] private int currentOption;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadItemOptionsAndButtons();
            LoadSpawners();
        }

        protected override void Start()
        {
            base.Start();
            AddClickListeners();
        }

        private void LoadItemOptionsAndButtons()
        {
            if(options.Count > 0 && buttons.Count > 0) return;

            options = GetComponentsInChildren<RectTransform>()
                    .Where(t => t != transform)
                    .ToList();
            buttons = GetComponentsInChildren<Button>().ToList();
        }

        private void LoadSpawners()
        {
            if(spawners.Count > 0) return;

            spawners = transform.parent.GetComponentsInChildren<Spawner>().ToList();
        }

        private void AddClickListeners()
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                int index = i;

                buttons[i].onClick.AddListener(() => Click(index));
            }
        }

        private void Click(int index)
        {
            if(currentOption == index) return;

            options[currentOption].sizeDelta = new Vector2(35f, 35f);

            spawners[currentOption].gameObject.SetActive(false);

            options[index].sizeDelta = new Vector2(50f, 50f);

            spawners[index].gameObject.SetActive(true);

            currentOption = index;
        }
    }
}