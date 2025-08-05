using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;


public class PlayerSpriteSwap : GMono
{
    [SerializeField] private SpriteLibrary mainSLB;
    [SerializeField] private EquipSpriteSetSO equipSets;
    protected PlayerSO playerData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteLibrary();
        LoadEquipSets();
    }

    protected override void Start()
    {
        base.Start();
        InitSet();
    }

    protected virtual void InitSet()
    {
        if (playerData.Helmet.Level > 0)
        {
            if (playerData.Helmet.EquipSO.SetId != 12)
                OverrideSet(playerData.Helmet.EquipSO.SetId, playerData.Helmet.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 0);
        }
            
        if (playerData.Armor.Level > 0)
        {
            if (playerData.Armor.EquipSO.SetId != 12)
                OverrideSet(playerData.Armor.EquipSO.SetId, playerData.Armor.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 1);
        }

        if (playerData.Armwear.Level > 0)
        {
            if (playerData.Armwear.EquipSO.SetId != 12)
                OverrideSet(playerData.Armwear.EquipSO.SetId, playerData.Armwear.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 2);
        }

        if (playerData.Boots.Level > 0)
        {
            if (playerData.Boots.EquipSO.SetId != 12)
                OverrideSet(playerData.Boots.EquipSO.SetId, playerData.Boots.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 3);
        }

        if (playerData.Weapon.Level > 0)
        {
            if (playerData.Weapon.EquipSO.SetId != 12)
                OverrideSet(playerData.Weapon.EquipSO.SetId, playerData.Weapon.EquipSO.PartIndex);
        }
        else
        {
            OverrideSet(0, 4);
        }
    }

    private void LoadSpriteLibrary()
    {
        if (mainSLB != null) return;

        mainSLB = GetComponent<SpriteLibrary>();
    }

    private void LoadEquipSets()
    {
        if(equipSets != null) return;

        equipSets = Resources.Load<EquipSpriteSetSO>("SpriteSwap/EquipSpriteSet"); 
    }

    protected void OverrideSet(int setId, int mainPartIndex)
    {
        var overrideSet = equipSets.AllSets.FirstOrDefault(s => s.SetId == setId);

        if(overrideSet == null || overrideSet.MainParts.Length < mainPartIndex) return;

        foreach(var part in overrideSet.MainParts[mainPartIndex].Parts)
        {
            mainSLB.AddOverride(part.Sprite, part.Category, part.Label);
        }
    }

    protected void ResetSet(int setId, int mainPartIndex)
    {
        var overrideSet = equipSets.AllSets.FirstOrDefault(s => s.SetId == setId);

        if(overrideSet == null || overrideSet.MainParts.Length < mainPartIndex) return;

        foreach(var part in overrideSet.MainParts[mainPartIndex].Parts)
        {
            mainSLB.RemoveOverride(part.Category, part.Label);
        }
    }
}