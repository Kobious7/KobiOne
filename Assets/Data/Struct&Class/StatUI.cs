using System;

[Serializable]
public class StatUI
{
    public int Index;
    public string Text;
    public int Data;

    public StatUI(int index, string text, int data)
    {
        Text = text;
        Data = data;
    }
}