using UnityEngine;

/// <summary>
/// Gắn vào GameObject chứa 1 SpriteRenderer tileable.
/// Script tự tạo 2 bản sao trái & phải, cuộn vô tận và có parallaxFactor.
/// </summary>
[ExecuteAlways]
public class InfiniteParallaxLayer : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float parallaxFactor = 0.5f;   // 0 = bám camera, 1 = bất động
    [SerializeField] private bool autoSetup = false;        // tick 1 lần trong Inspector để auto-clone

    private Camera cam;
    private float spriteWidth;
    private Vector3 lastCamPos;
    private Transform[] tiles;                              // 0=L, 1=C, 2=R
    private Vector3 layerStartPos;                          // vị trí gốc (y/z cố định)

    /* ───────── SETUP ───────── */

    private void OnValidate()
    {
        if (autoSetup)
        {
            autoSetup = false;
            SetupClones();
        }
    }

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null) return;

        layerStartPos = transform.position;
        lastCamPos    = cam.transform.position;

        if (tiles == null || tiles.Length != 3)
            SetupClones();
    }

    private void SetupClones()
    {
        // lấy (hoặc kiểm tra) SpriteRenderer gốc
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError($"[{name}] Không tìm thấy SpriteRenderer.");
            return;
        }

        spriteWidth = sr.bounds.size.x;

        // huỷ clones cũ (ngoại trừ bản gốc)
        for (int i = transform.parent.childCount - 1; i >= 0; i--)
        {
            Transform c = transform.parent.GetChild(i);
            if (c != transform && c.name.StartsWith($"{name}_Clone"))
                if (Application.isEditor) DestroyImmediate(c.gameObject);
                else                     Destroy(c.gameObject);
        }

        // tạo mảng & gán trung tâm
        tiles = new Transform[3];
        tiles[1] = transform;

        // tạo trái & phải
        tiles[0] = CreateClone(-spriteWidth, "_CloneL");
        tiles[2] = CreateClone( spriteWidth, "_CloneR");
    }

    private Transform CreateClone(float offsetX, string suffix)
    {
        GameObject clone = Instantiate(gameObject, transform.parent);
        clone.name = $"{name}{suffix}";
        DestroyImmediate(clone.GetComponent<InfiniteParallaxLayer>()); // clones không cần script
        clone.transform.localPosition = transform.localPosition + Vector3.right * offsetX;
        return clone.transform;
    }

    /* ───────── RUNTIME ───────── */

    private void LateUpdate()
    {
        if (cam == null || spriteWidth == 0f) return;

        /* 1️⃣  Di chuyển layer theo deltaCamera * parallaxFactor */
        Vector3 camDelta = cam.transform.position - lastCamPos;
        lastCamPos       = cam.transform.position;

        transform.position += new Vector3(camDelta.x * parallaxFactor, 0f, 0f);

        /* 2️⃣  Kiểm tra wrap vô tận */
        float camDist = cam.transform.position.x * parallaxFactor - transform.position.x;

        if (Mathf.Abs(camDist) >= spriteWidth)
        {
            int dir = camDist > 0f ? 1 : -1;                // 1 = qua phải, -1 = qua trái
            foreach (Transform t in tiles)
                t.position += Vector3.right * spriteWidth * dir;
        }

        /* 3️⃣  Giữ nguyên trục Y/Z ban đầu (đề phòng camera shake dọc) */
        transform.position = new Vector3(transform.position.x, layerStartPos.y, layerStartPos.z);
    }
}
