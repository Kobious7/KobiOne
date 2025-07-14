using System.Collections;
using UnityEngine;

public class BPlayerMovement : BEntityMovement
{
    private BPlayerAnim anim;

    protected override void Start()
    {
        base.Start();
        anim = Entity.Anim as BPlayerAnim;
        targetPos = BattleManager.Instance.Monster.transform;
    }
    protected override void GetTargetRadius()
    {
        targetRadius = BattleManager.Instance.Monster.CapsuleCollider.radius;
    }

    public IEnumerator SwordA1S_DashForward()
    {
        firstPos = transform.parent.position;
        anim.SwordA1SDashForward();

        while (Vector3.Distance(transform.parent.position, targetPos.position) > targetRadius + 2f)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPos.position, speed * Time.deltaTime);

            yield return null;
        }
    }

    public IEnumerator SwordA1S_MoveBack()
    {
        Entity.Model.localScale = new Vector3(-1, 1, 1);
        anim.SwordA1SMoveBack();

        while (Vector3.Distance(transform.parent.position, firstPos) > Time.deltaTime)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, firstPos, speed * Time.deltaTime);

            yield return null;
        }

        transform.parent.position = firstPos;
        Entity.Model.localScale = Vector3.one;
        anim.Idle();
    }
}