using System.Collections;
using UnityEngine;

public class ExtraFXObject : GMono
{
    [SerializeField] private Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        animator = GetComponent<Animator>();
    }

    public IEnumerator WaitAnim(string name)
    {
        yield return new WaitForSeconds(GetAnimDuration(name));
    }

    public float GetAnimDuration(string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }

        return 1;
    }
}