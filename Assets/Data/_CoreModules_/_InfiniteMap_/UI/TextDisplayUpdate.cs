using UnityEngine;


public class TextDisplayUpdate : TextDisplayAb
{
    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        text.Text.text = $"{(int)Game.Instance.Map.Distance}m";
    }
}