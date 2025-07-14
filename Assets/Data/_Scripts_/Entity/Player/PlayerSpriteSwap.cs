using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;


public class PlayerSpriteSwap : GMono
{
    [SerializeField] private SpriteLibrary mainSLB;
    [SerializeField] private EquipSpriteSetSO equipSets;
    private InfiniteMapManager infiniteMapManager;
    private InfiniteMapSO mapData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteLibrary();
        LoadEquipSets();
    }

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;
        mapData = infiniteMapManager.MapData;

        if (SceneManager.GetActiveScene().name == "InfiniteMap")
        {
            infiniteMapManager.Inventory.EquipWearing.OnEquipWearing += PartSetSwap;
        }

        if (mapData.Helmet.Level > 0)
        {
            OverrideSet(mapData.Helmet.EquipSO.SetId, mapData.Helmet.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 0);
        }
            
        if (mapData.Armor.Level > 0)
        {
            OverrideSet(mapData.Armor.EquipSO.SetId, mapData.Armor.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 1);
        }

        if (mapData.Armwear.Level > 0)
        {
            OverrideSet(mapData.Armwear.EquipSO.SetId, mapData.Armwear.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 2);
        }

        if (mapData.Boots.Level > 0)
        {
            OverrideSet(mapData.Boots.EquipSO.SetId, mapData.Boots.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 3);
        }

        if (mapData.Weapon.Level > 0)
        {
            OverrideSet(mapData.Weapon.EquipSO.SetId, mapData.Weapon.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 4);
        }

        //if (mapData.Special != null) OverrideSet(mapData.Special.EquipSO.SetId, mapData.Special.EquipSO.PartIndex);
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