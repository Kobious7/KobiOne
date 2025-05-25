using UnityEngine;

public class OpponentAnim : OpponentComponent
{
    [SerializeField] private bool idle = true;
    [SerializeField] private bool moveTT;
    [SerializeField] private bool moveB;

    public bool MoveB
    {
        get { return moveB; }
        set { moveB = value; }
    }

    [SerializeField] private bool attack;
    [SerializeField] private bool dealSlash;

    public void IdleAnim()
    {
        Opponent.Animator.SetInteger("state", 0);
    }

    public void RunAnim()
    {
        Opponent.Animator.SetInteger("state", 1);
        if (moveB) Opponent.Model.localScale = new Vector3(-1, 1, 1);
    }

    public void DealSlashAnim()
    {
        Opponent.Animator.SetTrigger("melee_attack");
    }

    public float GetAnimDuration(string name)
    {
        foreach (AnimationClip clip in Opponent.Animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name) return clip.length;
        }

        return 1;
    }
}