using UnityEngine;

public class PlayerAdjustCollider : GMono
{
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;
    [SerializeField] private Vector3 localPos;
    [SerializeField] private bool isOrigin;

    protected override void Start()
    {
        base.Start();
        AdjustCollider();
    }

    private void AdjustCollider()
    {
        if(transform.parent.parent.GetComponent<CapsuleCollider2D>() != null) isOrigin = true;

        if(isOrigin)
        {
            capsuleCollider2D = transform.parent.parent.GetComponent<CapsuleCollider2D>();
            capsuleCollider = transform.parent.parent.GetComponentInChildren<CapsuleCollider>();
        }
        else
        {
            capsuleCollider = transform.parent.parent.GetComponent<CapsuleCollider>();
        }

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        width = renderer.bounds.size.x;
        height = renderer.bounds.size.y - 0.1f;

        if (height > width)
        {
            
            capsuleCollider.direction = 1; // Y-axis for vertical
            capsuleCollider.height = height;
            capsuleCollider.radius = width / 2;

            if(isOrigin)
            {
                capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
                capsuleCollider2D.size = new Vector2(width, height);
            }
        }
        else
        {
            capsuleCollider.direction = 0; // X-axis for horizontal
            capsuleCollider.height = width;
            capsuleCollider.radius = height / 2;

            if(isOrigin)
            {
                capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
                capsuleCollider2D.size = new Vector2(height, width);
            }
        }

        localPos = transform.parent.parent.InverseTransformPoint(renderer.bounds.center);
        capsuleCollider.center = localPos;

        if(isOrigin)
        {
            capsuleCollider2D.offset = new Vector2(localPos.x, localPos.y);
        }

        transform.gameObject.SetActive(false);
    }
}