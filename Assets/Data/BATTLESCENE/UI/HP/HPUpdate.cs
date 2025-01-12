using UnityEngine;

namespace Battle
{
    public class HPUpdate : HPAb
    {
        [SerializeField] private float speed = 2;

        private void Update()
        {
            UpdateHP();
        }

        protected virtual void UpdateHP()
        {
            HPAbs.TextMP.text = GetNewString();
            HPAbs.Model.fillAmount = Mathf.Lerp(HPAbs.Model.fillAmount, GetNewFillAmount(), speed * Time.deltaTime);
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
}