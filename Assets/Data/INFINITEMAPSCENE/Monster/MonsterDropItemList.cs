using System.Collections.Generic;
using InfiniteMap;
using UnityEngine;

public class MonsterDropItemList : MonsterAb
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