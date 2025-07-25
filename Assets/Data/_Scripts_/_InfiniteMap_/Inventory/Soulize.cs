using System;
using System.Collections.Generic;
using UnityEngine;

public class Soulize : InventoryAb
{
    public event Action<InventoryEquip> OnEquipSoulized;
    public void SoulizeEquipment(InventoryEquip equip)
    {
        List<InventoryEquip> equipList = Inventory.GetEquipListByEquipType(equip.EquipSO.EquipType);

        equipList.Remove(equip);
        Inventory.PrimarionSoul += GetPrimarionSoulByRarity(equip.Rarity);
        OnEquipSoulized?.Invoke(equip);
        Inventory.CallPrimarionSoulChangedEvent();

        //Save Data
        InfiniteMapManager.Instance.PlayerData.PrimarionSoul = Inventory.PrimarionSoul;
    }

    private int GetPrimarionSoulByRarity(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Common => 2,
            Rarity.Uncommon => 5,
            Rarity.Rare => 10,
            Rarity.Epic => 30,
            Rarity.Lengendary => 70,
            Rarity.Mythic => 200,
            Rarity.Divine => 1000,
            Rarity.Infinity => 999999999,
            _ => 0
        };
    }
}