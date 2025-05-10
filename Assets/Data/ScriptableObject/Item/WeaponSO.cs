using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : EquipSO
{
    public WeaponType WeaponType;
    public AttackRange AttackRange;
}