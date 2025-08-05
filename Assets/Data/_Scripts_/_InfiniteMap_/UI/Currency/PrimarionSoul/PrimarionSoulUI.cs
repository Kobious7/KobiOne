using System.Collections;
using TMPro;
using UnityEngine;

public class PrimarionSoulUI : GMono
{
    [SerializeField] private TextMeshProUGUI value;
    private Inventory inventory;

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
        inventory = InfiniteMapManager.Instance.Inventory;
        inventory.OnPrimarionSoulChanged += UpdatePrimarionSoul;
        value.text = $"{inventory.PrimarionSoul}";
    }

    private void UpdatePrimarionSoul()
    {
        value.text = $"{inventory.PrimarionSoul}";
    }
}