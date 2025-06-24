using UnityEngine;

public class SwordS1ExtraFXObject : ExtraFXObject
{
    protected override void OnEnable()
    {
        base.OnEnable();
        
        StartCoroutine(WaitAnim("laser_fx_anim"));
        SkillObjectFXExtar.Instance.Despawn(transform);
    }

}
