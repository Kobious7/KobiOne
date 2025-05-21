using InfiniteMap;
using UnityEngine;

public class MonsterAnim : MonsterAb
{
    public void Idle()
    {
        Monster.Animator.SetInteger("state", 0);
    }

    public void Run()
    {
        Monster.Animator.SetInteger("state", 1);
    }
}