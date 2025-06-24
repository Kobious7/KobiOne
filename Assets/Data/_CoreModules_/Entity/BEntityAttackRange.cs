using UnityEngine;

public class BEntityAttackRange : EntityComponent
{
    [SerializeField] protected AttackRange current;

    public AttackRange Current => current;
}