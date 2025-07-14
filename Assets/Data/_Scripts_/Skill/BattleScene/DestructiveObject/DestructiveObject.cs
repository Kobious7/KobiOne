using UnityEngine;

public class DestructiveObject : GMono
{
    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private Transform target;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    [SerializeField] private float offsetValue;

    public float OffsetValue
    {
        get => offsetValue;
        set => offsetValue = value;
    }

    [SerializeField] private SpriteRenderer spriteRenderer;

    public SpriteRenderer SpriteRenderer => spriteRenderer;

    [SerializeField] private CapsuleCollider capsuleCollider;

    public CapsuleCollider CapsuleCollider => capsuleCollider;

    [SerializeField] private Transform hitFX;

    public Transform HitFX
    {
        get => hitFX;
        set => hitFX = value;
    }

    [SerializeField] private SkillButton skillButton;

    public SkillButton SkillButton
    {
        get => skillButton;
        set => skillButton = value;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadSpriteRender();
        LoadCapsuleCollider();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Model");
    }

    private void LoadSpriteRender()
    {
        if(spriteRenderer != null) return;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void LoadCapsuleCollider()
    {
        if(capsuleCollider != null) return;

        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void AdjustCollider()
    {
        // Get the sprite bounds
        float width = spriteRenderer.bounds.size.x;
        float height = spriteRenderer.bounds.size.y;

        // Set the capsule collider's direction and radius
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

        // Adjust the center to align with the sprite's position if needed
        capsuleCollider.center = Vector3.zero;
    }
}