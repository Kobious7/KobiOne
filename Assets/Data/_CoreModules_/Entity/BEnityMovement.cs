using System.Collections;
using UnityEngine;

public class BEntityMovement : EntityComponent
{
    [SerializeField] protected Transform targetPos;
    [SerializeField] protected Vector3 firstPos;
    [SerializeField] protected float speed;
    [SerializeField] protected float targetRadius;

    protected override void Start()
    {
        base.Start();
        GetTargetRadius();
    }

    public IEnumerator MoveToTarget()
    {
        firstPos = transform.parent.position;

        Entity.Anim.Run();

        while (Vector3.Distance(transform.parent.position, targetPos.position) > (2f + targetRadius))
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPos.position, speed * Time.deltaTime);

            yield return null;
        }
    }

    public IEnumerator MoveBack()
    {
        Entity.Model.localScale = new Vector3(-1, 1, 1);
        Entity.Anim.Run();

        while (Vector3.Distance(transform.parent.position, firstPos) > Time.deltaTime)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, firstPos, speed * Time.deltaTime);

            yield return null;
        }

        transform.parent.position = firstPos;
        Entity.Model.localScale = Vector3.one;
        Entity.Anim.Idle();
    }

    protected virtual void GetTargetRadius() { }
}