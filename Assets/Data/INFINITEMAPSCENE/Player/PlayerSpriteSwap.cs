using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace InfiniteMap
{
    public class PlayerSpriteSwap : GMono
    {
        [SerializeField] private SpriteLibrary mainSLB;
        [SerializeField] private EquipSpriteSetSO equipSets;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadSpriteLibrary();
            LoadEquipSets();
        }

        protected override void Start()
        {
            base.Start();
            
            Game.Instance.Inventory.EquipWearing.OnEquipWearing += PartSetSwap;
            
            for(int i = 0; i < 5; i++)
            {
                OverrideSet(0, i);
            }
        }

        private void LoadSpriteLibrary()
        {
            if(mainSLB != null) return;

            mainSLB = GetComponent<SpriteLibrary>();
        }

        private void LoadEquipSets()
        {
            if(equipSets != null) return;

            equipSets = Resources.Load<EquipSpriteSetSO>("SpriteSwap/EquipSpriteSet"); 
        }

        private void PartSetSwap(InventoryEquip equip)
        {
            EquipSO equipSO = equip.EquipSO;

            if(equipSO.SetId == 12)
            {
                ResetSet(equipSO.SetId, equipSO.PartIndex);
            }
            else
            {
                OverrideSet(equipSO.SetId, equipSO.PartIndex);
            }
        }

        public void OverrideSet(int setId, int mainPartIndex)
        {
            var overrideSet = equipSets.AllSets.FirstOrDefault(s => s.SetId == setId);

            if(overrideSet == null || overrideSet.MainParts.Length < mainPartIndex) return;

            foreach(var part in overrideSet.MainParts[mainPartIndex].Parts)
            {
                mainSLB.AddOverride(part.Sprite, part.Category, part.Label);
            }
        }

        public void ResetSet(int setId, int mainPartIndex)
        {
            var overrideSet = equipSets.AllSets.FirstOrDefault(s => s.SetId == setId);

            if(overrideSet == null || overrideSet.MainParts.Length < mainPartIndex) return;

            foreach(var part in overrideSet.MainParts[mainPartIndex].Parts)
            {
                mainSLB.RemoveOverride(part.Category, part.Label);
            }
        }
    }
}