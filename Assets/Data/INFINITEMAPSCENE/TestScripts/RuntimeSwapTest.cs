using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class RuntimeSwapTest : GMono
{
    [SerializeField] SwapGroup[] SwapGroups = null;
    [SerializeField] SpriteLibrary SpriteLibrary = null;

    protected override void Start()
    {
        base.Start();

        Invoke(nameof(CallOverride), 2f);
        Invoke(nameof(CallReset), 4f);
    }

    private void CallOverride()
    {
        OverrideEntry(0);
    }

    private void CallReset()
    {
        ResetEntry(0);
    }

    public void OverrideEntry(int i)
    {
        if(SwapGroups == null || SwapGroups.Length < i) return;

        foreach(var entry in SwapGroups[i].SwapEntries)
        {
            SpriteLibrary.AddOverride(entry.Sprite, entry.Category, entry.Label);
        }
    }

    public void ResetEntry(int i)
    {
        if(SwapGroups == null || SwapGroups.Length < i) return;

        foreach(var entry in SwapGroups[i].SwapEntries)
        {
            SpriteLibrary.RemoveOverride(entry.Category, entry.Label);
        }
    }
}