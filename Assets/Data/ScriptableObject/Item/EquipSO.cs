using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "ScriptableObjects/Equip")]
public class EquipSO : ScriptableObject
{
    public int ItemCode;
    public string ItemName;
    public Sprite Sprite;
    public EquipType EquipType;
    public float DropRate;
}