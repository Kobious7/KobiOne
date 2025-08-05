using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "ScriptableObjects/Equip")]
public class EquipSO : BaseItemSO
{
    public int SetId;
    public int PartIndex;
    public EquipType EquipType;
    public float DropRate;
}