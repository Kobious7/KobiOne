using UnityEngine;

public class PlayerSwordS1SSwordVFX : GMono
{
    [SerializeField] private Transform sword;
    [SerializeField] private Transform lightning;

    public void ActiveSword()
    {
        sword.gameObject.SetActive(true);
        sword.gameObject.SetActive(true);
    }

    public void InactiveSword()
    {
        sword.gameObject.SetActive(false);
        sword.gameObject.SetActive(false);
    }
}