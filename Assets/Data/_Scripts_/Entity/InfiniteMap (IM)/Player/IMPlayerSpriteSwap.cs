using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;


public class IMPlayerSpriteSwap : PlayerSpriteSwap
{
    private InfiniteMapManager infiniteMapManager;

    protected override void InitSet()
    {
        infiniteMapManager = InfiniteMapManager.Instance;
        playerData = infiniteMapManager.PlayerData;
        infiniteMapManager.Inventory.EquipWearing.OnEquipWearing += PartSetSwap;

        base.InitSet();
    }

    private void PartSetSwap(InventoryEquip equip)
    {
        EquipSO equipSO = equip.EquipSO;

        if (equipSO.SetId == 12)
        {
            ResetSet(equipSO.SetId, equipSO.PartIndex);
        }
        else
        {
            OverrideSet(equipSO.SetId, equipSO.PartIndex);
        }
    }
}