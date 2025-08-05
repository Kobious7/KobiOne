using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : BaseItemSO
{
    public ItemType ItemType;
    public float DropRate;
    public int MaxStack;
    public Rarity Rarity;
}
