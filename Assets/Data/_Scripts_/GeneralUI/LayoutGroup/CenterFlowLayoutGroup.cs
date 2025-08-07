using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class CenterFlowLayoutGroup : LayoutGroup
{
    public float Spacing = 10f;
    public float MaxWidth = 700f;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        Arrange();
    }

    public override void CalculateLayoutInputVertical()
    {
        Arrange();
    }

    public override void SetLayoutHorizontal() { }
    public override void SetLayoutVertical() { }

    private void Arrange()
    {
        if (rectChildren.Count == 0)
            return;

        float yOffset = 0f;
        float lineHeight = 0f;

        List<List<RectTransform>> lines = new List<List<RectTransform>>();
        List<RectTransform> currentLine = new List<RectTransform>();
        float currentLineWidth = 0f;

        foreach (RectTransform child in rectChildren)
        {
            float childWidth = LayoutUtility.GetPreferredSize(child, 0);
            float childHeight = LayoutUtility.GetPreferredSize(child, 1);
            float spacing = currentLine.Count > 0 ? Spacing : 0f;

            if (currentLineWidth + childWidth + spacing > MaxWidth && currentLine.Count > 0)
            {
                lines.Add(currentLine);
                yOffset += lineHeight + Spacing;
                currentLine = new List<RectTransform>();
                currentLineWidth = 0f;
                lineHeight = 0f;
                spacing = 0f;
            }

            currentLine.Add(child);
            currentLineWidth += childWidth + spacing;
            lineHeight = Mathf.Max(lineHeight, childHeight);
        }

        if (currentLine.Count > 0)
        {
            lines.Add(currentLine);
        }

        yOffset = 0f;
        foreach (var line in lines)
        {
            float lineWidth = GetTotalWidth(line);
            float startX = (rectTransform.rect.width - lineWidth) * GetAlignmentPivot(childAlignment).x;
            float currentX = startX;

            float maxHeight = GetMaxHeight(line);
            float posY = (rectTransform.rect.height - maxHeight) * (1f - GetAlignmentPivot(childAlignment).y);

            foreach (RectTransform child in line)
            {
                float childWidth = LayoutUtility.GetPreferredSize(child, 0);
                float childHeight = LayoutUtility.GetPreferredSize(child, 1);

                SetChildAlongAxis(child, 0, currentX);
                SetChildAlongAxis(child, 1, posY + yOffset);

                currentX += childWidth + Spacing;
            }

            yOffset += maxHeight + Spacing;
        }
    }

    private float GetTotalWidth(List<RectTransform> line)
    {
        float width = 0f;
        for (int i = 0; i < line.Count; i++)
        {
            width += LayoutUtility.GetPreferredSize(line[i], 0);
            if (i < line.Count - 1)
                width += Spacing;
        }
        return width;
    }

    private float GetMaxHeight(List<RectTransform> line)
    {
        float max = 0f;
        foreach (RectTransform child in line)
        {
            max = Mathf.Max(max, LayoutUtility.GetPreferredSize(child, 1));
        }
        return max;
    }

    private Vector2 GetAlignmentPivot(TextAnchor anchor)
    {
        switch (anchor)
        {
            case TextAnchor.UpperLeft: return new Vector2(0f, 1f);
            case TextAnchor.UpperCenter: return new Vector2(0.5f, 1f);
            case TextAnchor.UpperRight: return new Vector2(1f, 1f);
            case TextAnchor.MiddleLeft: return new Vector2(0f, 0.5f);
            case TextAnchor.MiddleCenter: return new Vector2(0.5f, 0.5f);
            case TextAnchor.MiddleRight: return new Vector2(1f, 0.5f);
            case TextAnchor.LowerLeft: return new Vector2(0f, 0f);
            case TextAnchor.LowerCenter: return new Vector2(0.5f, 0f);
            case TextAnchor.LowerRight: return new Vector2(1f, 0f);
        }
        return new Vector2(0.5f, 0.5f);
    }
}
