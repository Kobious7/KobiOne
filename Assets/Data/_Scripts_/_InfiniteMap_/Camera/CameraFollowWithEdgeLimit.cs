using UnityEngine;

public class CameraFollowWithEdgeLimit : GMono
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Follow Offset")]
    public float offsetX = 0f;
    public float offsetY = 0f;

    [Header("X Axis Settings")]
    public bool lockX = true;
    public float stopLeft = -10f;
    public float stopRight = 20f;

    [Header("Y Axis Settings")]
    public bool lockY = false;
    public float stopBottom = -5f;
    public float stopTop = 10f;

    private Camera cam;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        cam = Camera.main;
    }

    protected override void Start()
    {
        base.Start();

        target = InfiniteMapManager.Instance.Player.transform;
    }

    private void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 camPos = transform.position;

        // Lấy vị trí có offset
        Vector3 targetPos = target.position + new Vector3(offsetX, offsetY, 0f);

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        float newX = camPos.x;
        float newY = camPos.y;

        // X follow
        if (lockX)
        {
            float leftEdge = targetPos.x - camWidth / 2f;
            float rightEdge = targetPos.x + camWidth / 2f;

            if (leftEdge >= stopLeft && rightEdge <= stopRight)
            {
                newX = targetPos.x;
            }
        }
        else
        {
            newX = targetPos.x;
        }

        // Y follow
        if (lockY)
        {
            float bottomEdge = targetPos.y - camHeight / 2f;
            float topEdge = targetPos.y + camHeight / 2f;

            if (bottomEdge >= stopBottom && topEdge <= stopTop)
            {
                newY = targetPos.y;
            }
        }
        else
        {
            newY = targetPos.y;
        }

        // Gán vị trí mới
        transform.position = new Vector3(newX, newY, camPos.z);
    }
}
