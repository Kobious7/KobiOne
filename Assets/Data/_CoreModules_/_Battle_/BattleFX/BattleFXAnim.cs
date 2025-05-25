using UnityEngine;

public class BattleFXAnim : BattleFXAb
{
    public float GetAnimClipTime()
    {
        return GetAnimationClipLength(FX.Animator, transform.parent.name);
    }

    private float GetAnimationClipLength(Animator animator, string name)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
                return clip.length;
        }

        return 0;
    }
}