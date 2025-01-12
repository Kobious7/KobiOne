using UnityEngine;

namespace Battle
{
    public class BoardAb : GMono
    {
        [SerializeField] protected Board Board;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadBoard();
        }

        private void LoadBoard()
        {
            if (Board != null) return;

            Board = transform.parent.GetComponent<Board>();
        }
    }
}