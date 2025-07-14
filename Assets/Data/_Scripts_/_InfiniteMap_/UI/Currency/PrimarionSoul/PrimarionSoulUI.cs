using System.Collections;
using TMPro;
using UnityEngine;

public class PrimarionSoul : GMono
{
    [SerializeField] private TextMeshProUGUI value;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        value = transform.Find("Value").GetComponent<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        value.text = $"{InfiniteMapManager.Instance.Inventory.PrimarionSoul}";
    }
}