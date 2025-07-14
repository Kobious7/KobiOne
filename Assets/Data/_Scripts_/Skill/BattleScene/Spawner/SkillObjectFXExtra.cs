using System.Collections.Generic;
using UnityEngine;

public class SkillObjectFXExtar : PrefabSpawner
{
    private static SkillObjectFXExtar instance;

    public static SkillObjectFXExtar Instance => instance;
    
    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 SkillObjectFXExtar is allowed to exist!");

        instance = this;
    }
}