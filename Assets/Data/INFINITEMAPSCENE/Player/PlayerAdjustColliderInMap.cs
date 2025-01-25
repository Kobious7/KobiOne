using UnityEngine;

public class PlayerAdjustColliderInMap : GMono
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;
    [SerializeField] private Bounds bounds;
    [SerializeField] private Vector3 localPos;

    protected override void Start()
    {
        base.Start();
        AdjustCollider();
    }

    private void AdjustCollider()
    {
        Debug.Log(transform.parent.parent);
        capsuleCollider = this.transform.parent.parent.GetComponentInChildren<CapsuleCollider>();
        capsuleCollider2D = this.transform.parent.parent.GetComponent<CapsuleCollider2D>();
        renderers = gameObject.GetComponentsInChildren<Renderer>();
        Transform attackPoint = transform.parent.parent.Find("AttackPoint");
        Transform centerPoint = transform.parent.parent.Find("CenterPoint");

        if (renderers.Length < 0) return;

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

            capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
            capsuleCollider2D.size = new Vector2(width, height);
        }
        else
        {
            capsuleCollider.direction = 0; // X-axis for horizontal
            capsuleCollider.height = width;
            capsuleCollider.radius = height / 2;

            capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
            capsuleCollider2D.size = new Vector2(height, width);
        }

        localPos = transform.InverseTransformPoint(bounds.center);
        capsuleCollider.center = localPos;
        capsuleCollider2D.offset = new Vector2(localPos.x, localPos.y);
        if(centerPoint == null || attackPoint == null) return;
        centerPoint.position = to2DVec(bounds.center);
        attackPoint.position = to2DVec(bounds.center) + new Vector3(1, 0, 0);
    }
}