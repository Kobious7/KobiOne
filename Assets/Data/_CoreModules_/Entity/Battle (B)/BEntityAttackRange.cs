using UnityEngine;

public class BEntityAttackRange : BEntityComponent
{
    [SerializeField] protected AttackRange current;

    public AttackRange Current => current;
}