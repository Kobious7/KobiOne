using UnityEngine;

public class WHMeasurement : MonoBehaviour
{
    public float width, height;

    void Reset()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null)
        {
            Debug.LogWarning("Không tìm thấy SpriteRenderer hoặc sprite.");
            return;
        }

        // Kích thước gốc của sprite (unit world)
        Vector2 spriteSize = sr.sprite.bounds.size;

        // Scale thực tế (bao gồm cả parent nếu có)
        Vector3 scale = transform.lossyScale;

        // Kích thước hiển thị thực tế
        width = spriteSize.x * scale.x;
        height = spriteSize.y * scale.y;

        // width = spriteSize.x;
        // height = spriteSize.y;

        Debug.Log($"Kích thước thực tế của sprite: Width = {width}, Height = {height}");
    }
}
