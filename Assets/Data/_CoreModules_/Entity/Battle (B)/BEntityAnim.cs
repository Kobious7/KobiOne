using System.Collections;
using UnityEngine;

public class BEntityAnim : BEntityComponent
{
    public void Idle()
    {
        Entity.Animator.SetInteger("state", 0);
    }

    public void Run()
    {
        Entity.Animator.SetInteger("state", 1);
    }

    public IEnumerator WaitAnim(string name)
    {
        yield return new WaitForSeconds(GetAnimDuration(name));
    }

    public float GetAnimDuration(string name)
    {
        foreach (AnimationClip clip in Entity.Animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }

        return 1;
    }
}