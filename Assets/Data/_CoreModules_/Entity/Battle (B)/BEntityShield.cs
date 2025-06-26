using System.Collections;
using UnityEngine;

public class BEntityShield : BEntityComponent
{
    [SerializeField] private Animator animator;

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

    public void ShieldAppear()
    {
        transform.gameObject.SetActive(true);
        animator.Play("appear");
    }

    public void ShieldContinous()
    {
        animator.SetInteger("state", 0);
    }

    public IEnumerator ShieldHit()
    {
        animator.SetInteger("state", 1);
        yield return StartCoroutine(WaitAnim("hit"));
    }

    public IEnumerator ShieldBreak()
    {
        animator.SetInteger("state", 2);
        yield return StartCoroutine(WaitAnim("dissapear"));
        transform.gameObject.SetActive(false);
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