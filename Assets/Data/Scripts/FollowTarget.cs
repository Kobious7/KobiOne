using UnityEngine;

public class FollowTarget : GMono
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = to2DVec(target.position);
    }
}