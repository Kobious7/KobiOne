using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;

public class Testttt : GMono
{
    [SerializeField] private SpriteLibrary mainSLB;
    [SerializeField] private EquipSpriteSetSO equipSpriteSetSO;

    protected override void Start()
    {
        base.Start();

        Invoke(nameof(CallOverride), 2f);
        //Invoke(nameof(CallReset), 4f);
    }

    private void CallOverride()
    {
        OverrideSet(11, 1);
    }

    private void CallReset()
    {
        ResetSet(11, 0);
    }

    public void OverrideSet(int setId, int mainPartIndex)
    {
        var overrideSet = equipSpriteSetSO.AllSets.FirstOrDefault(s => s.SetId == setId);

        if(overrideSet == null || overrideSet.MainParts.Length < mainPartIndex) return;

        foreach(var part in overrideSet.MainParts[mainPartIndex].Parts)
        {
            mainSLB.AddOverride(part.Sprite, part.Category, part.Label);
        }
    }

    public void ResetSet(int setId, int mainPartIndex)
    {
        var overrideSet = equipSpriteSetSO.AllSets.FirstOrDefault(s => s.SetId == setId);

        if(overrideSet == null || overrideSet.MainParts.Length < mainPartIndex) return;

        foreach(var part in overrideSet.MainParts[mainPartIndex].Parts)
        {
            mainSLB.RemoveOverride(part.Category, part.Label);
        }
    }
}