using UnityEngine;

public class CameraFollowWithEdgeLimit : GMono
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Follow Offset")]
    public float offsetX = 0f;
    public float offsetY = 0f;

    [Header("Lock X Left Settings")]
    public bool lockXLeft = true;
    public float stopLeft = -10f;

    private Camera cam;
    private InfiniteMapManager infiniteMapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        cam = Camera.main;
    }

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;
        target = infiniteMapManager.Player.transform;
    }

    private void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 camPos = transform.position;
        Vector3 targetPos = target.position + new Vector3(offsetX, offsetY, 0f);
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;
        float newX = camPos.x;
        float newY = camPos.y;

        // X follow
        if (lockXLeft && infiniteMapManager.Map.Distance <= 0)
        {
            stopLeft = infiniteMapManager.Player.Movement.XStop - 1f;
            float leftEdge = targetPos.x - camWidth / 2f;

            if (leftEdge >= stopLeft)
            {
                newX = targetPos.x;
            }
            else
            {
                newX = stopLeft + camWidth / 2;
            }
        }
        else
        {
            newX = targetPos.x;
        }

        newY = targetPos.y;

        // Gán vị trí mới
        transform.position = new Vector3(newX, newY, camPos.z);
    }
}
