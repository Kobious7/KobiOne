using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EntityText : GMono
{
    [SerializeField] private TextMeshProUGUI hp;

    public TextMeshProUGUI HP
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] private TextMeshProUGUI vHP;

    public TextMeshProUGUI VHP
    {
        get { return vHP; }
        set { vHP = value; }
    }

    [SerializeField] private TextMeshProUGUI mp;

    public TextMeshProUGUI MP
    {
        get { return mp; }
        set { mp = value; }
    }

    [SerializeField] private TextMeshProUGUI shieldCount;

    public TextMeshProUGUI ShieldCount
    {
        get { return shieldCount; }
        set { shieldCount = value; }
    }

    [SerializeField] private TextMeshProUGUI shieldStack;

    public TextMeshProUGUI ShieldStack
    {
        get { return shieldStack; }
        set { shieldStack = value; }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHP();
        LoadVHP();
        LoadMP();
        LoadShieldCount();
        LoadShieldStack();
    }

    private void LoadHP()
    {
        if (hp != null) return;

        hp = transform.Find("HP").GetComponent<TextMeshProUGUI>();
    }

    private void LoadVHP()
    {
        if (vHP != null) return;

        vHP = transform.Find("VHP").GetComponent<TextMeshProUGUI>();
    }

    private void LoadMP()
    {
        if (mp != null) return;

        mp = transform.Find("MP").GetComponent<TextMeshProUGUI>();
    }

    private void LoadShieldCount()
    {
        if (shieldCount != null) return;

        shieldCount = transform.Find("ShieldCount").GetComponent<TextMeshProUGUI>();
    }

    private void LoadShieldStack()
    {
        if (shieldStack != null) return;

        shieldStack = transform.Find("ShieldStack").GetComponent<TextMeshProUGUI>();
    }
}