using UnityEngine;

public class BMonsterStats : BEntityStats
{
    protected override void Start()
    {
        base.Start();

        InitBuff();
    }
}