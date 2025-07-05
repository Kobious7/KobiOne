using System.Collections.Generic;
using UnityEngine;

public class IMMonsterDropList : EntityComponent
{
    [SerializeField] private List<ItemSO> itemDropList;

    public List<ItemSO> ItemDropList
    {
        get => itemDropList;
        set => itemDropList = value;
    }

    [SerializeField] private List<EquipSO> equipDropList;

    public List<EquipSO> EquipDropList
    {
        get => equipDropList;
        set => equipDropList = value;
    }
}