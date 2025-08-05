using UnityEngine;

public class ParallaxEffect : GMono
{
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private Transform cam;
    [SerializeField] private float length;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Transform leftCopy, rightCopy;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        if (leftCopy == null) leftCopy = transform.Find("LeftCopy");
        if (rightCopy == null) rightCopy = transform.Find("RightCopy");
    }

    protected override void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position;
        leftCopy.localPosition = new Vector3(length / 2, 0, 0);
        rightCopy.localPosition = new Vector3(-length / 2, 0, 0);
    }

    private void FixedUpdate()
    {
        float distance = cam.position.x * (1 - parallaxMultiplier);
        float parallax = cam.position.x * parallaxMultiplier;
        transform.position = new Vector3(startPos.x + parallax, transform.position.y, transform.position.z);

        if(distance > startPos.x + length) startPos.x += length;
        else if(distance < startPos.x - length) startPos.x -= length;
    }
}