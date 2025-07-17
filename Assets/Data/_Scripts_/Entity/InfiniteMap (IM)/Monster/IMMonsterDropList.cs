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

    [Header("1x Drop")]
    [SerializeField] private List<ItemSO> itemDropList1x;
    [SerializeField] private List<EquipSO> equipDropList1x;

    [Header("2x Drop")]
    [SerializeField] private List<ItemSO> itemDropList2x;
    [SerializeField] private List<EquipSO> equipDropList2x;

    [Header("3x Drop")]
    [SerializeField] private List<ItemSO> itemDropList3x;
    [SerializeField] private List<EquipSO> equipDropList3x;

    [Header("4x Drop")]
    [SerializeField] private List<ItemSO> itemDropList4x;
    [SerializeField] private List<EquipSO> equipDropList4x;

    [Header("5x Drop")]
    [SerializeField] private List<ItemSO> itemDropList5x;
    [SerializeField] private List<EquipSO> equipDropList5x;

    [Header("6x Drop")]
    [SerializeField] private List<ItemSO> itemDropList6x;
    [SerializeField] private List<EquipSO> equipDropList6x;

    [Header("7x Drop")]
    [SerializeField] private List<ItemSO> itemDropList7x;
    [SerializeField] private List<EquipSO> equipDropList7x;

    [Header("8x Drop")]
    [SerializeField] private List<ItemSO> itemDropList8x;
    [SerializeField] private List<EquipSO> equipDropList8x;

    [Header("9x Drop")]
    [SerializeField] private List<ItemSO> itemDropList9x;
    [SerializeField] private List<EquipSO> equipDropList9x;

    [Header("10x Drop")]
    [SerializeField] private List<ItemSO> itemDropList10x;
    [SerializeField] private List<EquipSO> equipDropList10x;

    public List<ItemSO> GetItemDropList(int level) => level switch
    {
        >= 100 when itemDropList10x.Count > 0 => itemDropList10x,
        >= 90 when itemDropList9x.Count > 0 => itemDropList9x,
        >= 80 when itemDropList8x.Count > 0 => itemDropList8x,
        >= 70 when itemDropList7x.Count > 0 => itemDropList7x,
        >= 60 when itemDropList6x.Count > 0 => itemDropList6x,
        >= 50 when itemDropList5x.Count > 0 => itemDropList5x,
        >= 40 when itemDropList4x.Count > 0 => itemDropList4x,
        >= 30 when itemDropList3x.Count > 0 => itemDropList3x,
        >= 20 when itemDropList2x.Count > 0 => itemDropList2x,
        >= 10 when itemDropList1x.Count > 0 => itemDropList1x,
        _ => new List<ItemSO>()
    };

    public List<EquipSO> GetEquipDropList(int level) => level switch
    {
        >= 100 when equipDropList10x.Count > 0 => equipDropList10x,
        >= 90  when equipDropList9x.Count > 0 => equipDropList9x,
        >= 80  when equipDropList8x.Count > 0 => equipDropList8x,
        >= 70  when equipDropList7x.Count > 0 => equipDropList7x,
        >= 60  when equipDropList6x.Count > 0 => equipDropList6x,
        >= 50  when equipDropList5x.Count > 0 => equipDropList5x,
        >= 40  when equipDropList4x.Count > 0 => equipDropList4x,
        >= 30  when equipDropList3x.Count > 0 => equipDropList3x,
        >= 20  when equipDropList2x.Count > 0 => equipDropList2x,
        >= 10  when equipDropList1x.Count > 0 => equipDropList1x,
        _ => null
    };
}