using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "ScriptableObjects/Equip")]
public class EquipSO : ScriptableObject
{
    public int ItemCode;
    public string ItemName;
    public int SetId;
    public int PartIndex;
    public Sprite Sprite;
    public EquipType EquipType;
    public float DropRate;
}