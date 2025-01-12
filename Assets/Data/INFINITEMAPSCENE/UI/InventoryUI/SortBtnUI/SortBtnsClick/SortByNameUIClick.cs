using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InfiniteMap
{ 
    public class SortByNameUIClick : ButtonUIClick
    {
        protected override void Click()
        {
            base.Click();
            Debug.Log("Click!");
            SortByName();
        }

        private void SortByName()
        {
            List<ItemUI> items = ItemUISpawner.Instance.Holder.GetComponentsInChildren<ItemUI>().ToList();
            List<Vector3> pos = items.Select(x => x.transform.position).ToList();

            if (items.Count <= 0) return;

            var sortList = items.OrderBy(i => i.ItemSO.ItemName).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                sortList[i].transform.position = pos[i];
            }
        }
    }
}
