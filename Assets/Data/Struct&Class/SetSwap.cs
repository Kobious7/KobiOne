using System;
using UnityEngine;

[Serializable]
public class SetSwap
{
    [Serializable]
    public class PartSwap
    {
        public Sprite Sprite;
        public string Category;
        public string Label;
    }

    [Serializable]
    public class MainPartSwap
    {
        public string PartName;
        public PartSwap[] Parts;
    }

    public string SetName;
    public int SetId;
    public MainPartSwap[] MainParts;
}