using UnityEngine;

public class DestructiveObjectFlying : DestructiveObjectAb
{
    [SerializeField] private float speed = 2;

    private void Update()
    {
        if (DObject.Target == null) return;

        LockTarget();
        Fly();
    }

    public void Fly()
    {
        Vector3 targetPosWithOffset = new Vector3(DObject.Target.position.x - DObject.OffsetValue,
                                        DObject.Target.position.y + DObject.OffsetValue,
                                        DObject.Target.transform.position.z);

        if (Vector3.Distance(transform.parent.position, targetPosWithOffset) > Time.deltaTime)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, targetPosWithOffset, speed * Time.deltaTime);
        }
    }

    public void LockTarget()
    {
        Vector3 direction = DObject.Target.position - transform.parent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}