using System;

[Serializable]
public class StatUI
{
    public string Text;
    public int Value;

    public StatUI(string text, int value)
    {
        Text = text;
        Value = value;
    }
}