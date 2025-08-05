using Cinemachine;
using UnityEngine;

public class VirtualCameraFollowSetting : GMono
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float stopFollowX = -10f; // Giới hạn bên trái (rìa trái camera tới đây thì dừng follow)
    [SerializeField] private Transform player;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (virtualCamera == null) virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    protected override void Start()
    {
        base.Start();

        player = InfiniteMapManager.Instance.Player.transform;
        virtualCamera.Follow = player;
    }

    private void Update()
    {
        CheckStopFollowByLeftEdge();
    }

    private void CheckStopFollowByLeftEdge()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float leftEdgeX = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane)).x;

        if (leftEdgeX <= stopFollowX)
        {
            virtualCamera.Follow = null;
        }
        
        if (virtualCamera.Follow == null && player.position.x > stopFollowX + 10)
        {
            Debug.Log("Me");
            virtualCamera.Follow = player;
        }
    }
}
