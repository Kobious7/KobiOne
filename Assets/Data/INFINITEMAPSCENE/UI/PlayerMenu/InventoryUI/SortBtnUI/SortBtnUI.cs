using UnityEngine;

namespace InfiniteMap
{
    public class SortBtnUI : ButtonUI
    {
        [SerializeField] private Transform sortBoard;

        public Transform SortBoard => sortBoard;

        [SerializeField] private bool closed = true;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSortBoard();
        }

        protected override void Start()
        {
            base.Start();
            btn.onClick.AddListener(Click);
        }

        private void LoadSortBoard()
        {
            if(sortBoard != null) return;

            sortBoard = transform.Find("TypeSort");
        }

        private void Click()
        {
            if(closed)
            {
                sortBoard.gameObject.SetActive(true);
                closed = false;
            }
            else
            {
                sortBoard.gameObject.SetActive(false);
                closed = true;
            }
        }
    }
}