using System.Collections;
using UnityEngine;

public class BSkillFX : GMono
{
    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
    }

    private void LoadAnimator()
    {
        if (animator != null) return;

        animator = GetComponentInChildren<Animator>();
    }

    public IEnumerator WaitHitFX()
    {
        yield return new WaitForSeconds(GetAnimDuration("Hit"));
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