using UnityEngine;

public abstract class MapAb : GMono
{
    [SerializeField] private Map map;

    public Map Map => map;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMap();
    }

    private void LoadMap()
    {
        if (map != null) return;

        map = transform.parent.GetComponent<Map>();
    }
}
