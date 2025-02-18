using UnityEngine;
using UnityEngine.UI;

namespace InfiniteMap
{
    public class DragUIBtnClose : GMono
    {
        [SerializeField] private Button button;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadButton();
        }

        protected override void Start()
        {
            base.Start();
            AddClickListener();
        }

        private void LoadButton()
        {
            if (button != null) return;

            button = GetComponent<Button>();
        }

        private void AddClickListener()
        {
            button.onClick.AddListener(Click);
        }

        private void Click()
        {
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}