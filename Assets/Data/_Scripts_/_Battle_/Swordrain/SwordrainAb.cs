using UnityEngine;

public class SwordrainAb : GMono
{
    [SerializeField] private Swordrain swordrain;

    public Swordrain Swordrain => swordrain;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSwordrain();
    }

    private void LoadSwordrain()
    {
        if (swordrain != null) return;

        swordrain = transform.parent.GetComponent<Swordrain>();
    }
}