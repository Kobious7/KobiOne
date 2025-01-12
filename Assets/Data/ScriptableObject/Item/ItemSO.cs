using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public int ItemCode;
    public string ItemName;
    public Sprite Sprite;
    public ItemType ItemType;
    public float DropRate;    
    public int MaxStack;
}
