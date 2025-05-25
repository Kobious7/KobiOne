using UnityEngine;

public class MPUpdate : MPAb
{
    [SerializeField] private float speed = 2;

    private void Update()
    {
        UpdateMP();
    }

    protected virtual void UpdateMP()
    {
        MPAbs.TextMP.text = GetNewString();
        MPAbs.Model.fillAmount = Mathf.Lerp(MPAbs.Model.fillAmount, GetNewFillAmount(), speed * Time.deltaTime);
    }

    protected virtual float GetNewFillAmount()
    {
        return 0;
    }

    protected virtual string GetNewString()
    {
        return "";
    }
}