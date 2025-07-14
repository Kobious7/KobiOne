using UnityEngine;

public class OpText : EntityText
{
    private static OpText instance;

    public static OpText Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 OpText exists!");

        instance = this;
    }
}