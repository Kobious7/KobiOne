using System.Collections.Generic;

public interface IEntityBattleStats
{
    int Attack { get; set; }
    int MagicAttack { get; set; }
    int CurrentHP { get; set; }
    int MaxHP { get; set; }
    int Defense { get; set; }
    int Accuracy { get; set; }
    int SlashDamage { get; set; }
    int SwordrainDamage { get; set; }
    float DamageRange { get; set; }
    float CritRate { get; set; }
    float CritDamage { get; set; }
    float ManaRegen { get; set; }
    int VHP { get; set; }
    int Mana { get; set; }
    int ShieldCount { get; set; }
    int ShieldStack { get; set; }
    Dictionary<EquipStatType, StatBuffInfo> BuffPercents { get; set; }
    void HPIns(int amount);
    void HPDes(int amount);
    void VHPIns(int amount);
    void VHPDes(int amount);
    void ManaIns(int percent);
    void ManaDes(int percent);
    void SheildStack(int count);
    int DamageCalculate(int rawDamage, IEntityBattleStats opStats);
    bool CheckCrit();
    void DealDamage(int rawDamage, IEntityBattleStats receiver);
}