using System.Collections;
using Battle;
using UnityEngine;

public class PlayerAdjustColliderInBattle : GMono
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Bounds bounds;
    [SerializeField] private Vector3 localPos;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AdjustCollider());
    }

    private IEnumerator a()
    {
        yield return null;

        Debug.Log(transform.parent.parent.name);
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
        height = combinedBounds.size.y - 0.1f;
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