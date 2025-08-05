using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrailShop", menuName = "ScriptableObjects/TrailShop")]
public class TrailShopSO : ScriptableObject
{
    public List<TrailShopItem> ItemList;
}