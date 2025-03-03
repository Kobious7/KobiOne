using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace InfiniteMap
{
    public class PlayerSpriteSwap : GMono
    {
        [SerializeField] private SpriteLibrary spriteLibrary;
        [SerializeField] private List<PartSwap> partSwaps;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSpriteLibrary();
            LoadPartSwap();
        }

        private void LoadSpriteLibrary()
        {
            if(spriteLibrary != null) return;

            spriteLibrary = GetComponent<SpriteLibrary>();
        }

        private void LoadPartSwap()
        {
            partSwaps = new();
            PartSwap helmet = LoadPart("Head1", "Helmet");
            PartSwap weapon = LoadPart("weapon", "Weapon");

            partSwaps.Add(helmet);
            partSwaps.Add(weapon);
        }

        private PartSwap LoadPart(string partName, string categoryName)
        {
            PartSwap partSwap = new PartSwap();
            partSwap.SpriteResolver = transform.Find(partName).GetComponent<SpriteResolver>();
            partSwap.Category = categoryName;

            return partSwap;
        }

        public void SetSpriteResolver(int index, string label)
        {
            partSwaps[index].SpriteResolver.SetCategoryAndLabel(partSwaps[index].Category, label);
        }
    }
}