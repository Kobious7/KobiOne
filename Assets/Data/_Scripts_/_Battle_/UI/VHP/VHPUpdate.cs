using UnityEngine;

public class VHPUpdate : VHPAb
{
    [SerializeField] private float speed = 2;

    private void Update()
    {
        UpdateMP();
    }

    protected virtual void UpdateMP()
    {
        VHP.Model.fillAmount = Mathf.Lerp(VHP.Model.fillAmount, GetNewFillAmount(), speed * Time.deltaTime);
    }

    protected virtual float GetNewFillAmount()
    {
        return 0;
    }
}