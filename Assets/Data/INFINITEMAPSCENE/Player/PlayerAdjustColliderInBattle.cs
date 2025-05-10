using System.Collections;
using Battle;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAdjustColliderInBattle : GMono
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Bounds bounds;
    [SerializeField] private Vector3 localPos;
    private Vector3 ground;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AdjustPlaterPos());
    }

    private IEnumerator AdjustPlaterPos()
    {
        yield return StartCoroutine(AdjustCollider());

        ground = new Vector3(-8, -4, 0);
        Vector3 localGround = transform.InverseTransformPoint(ground);
        float yCurrentGround = localPos.y - height / 2;
        float distanceFromGround = yCurrentGround - localGround.y;
        transform.parent.parent.position = new Vector3(ground.x, transform.parent.parent.position.y - distanceFromGround, 0);
    }

    private IEnumerator AdjustCollider()
    {
        yield return null;

        capsuleCollider = transform.parent.parent.GetComponent<CapsuleCollider>();
        renderers = gameObject.GetComponentsInChildren<Renderer>();

        if (renderers.Length <= 0) yield break;

        Bounds combinedBounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            if(renderer.transform.name != "weapon")
                combinedBounds.Encapsulate(renderer.bounds);
        }

        width = combinedBounds.size.x;
        height = combinedBounds.size.y;
        bounds = combinedBounds;

        if (height > width)
        {
            capsuleCollider.direction = 1; // Y-axis for vertical
            capsuleCollider.height = height;
            capsuleCollider.radius = width / 2;
        }
        else
        {
            capsuleCollider.direction = 0; // X-axis for horizontal
            capsuleCollider.height = width;
            capsuleCollider.radius = height / 2;
        }

        localPos = transform.InverseTransformPoint(bounds.center);
        capsuleCollider.center = localPos;
    }
}