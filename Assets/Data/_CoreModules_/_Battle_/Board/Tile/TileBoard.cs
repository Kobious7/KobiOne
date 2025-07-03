using UnityEngine;

public class TileBoard : GMono
{
    [SerializeField] private float defaultDespawnDuration = 0.5f;

    public float DefaultDespawnDuration => defaultDespawnDuration;

    [SerializeField] private Transform model;

    public Transform Model => model;

    [SerializeField] private TileProperties tileProperties;

    public TileProperties TileProperties => tileProperties;

    [SerializeField] private TileMoving tileMoving;

    public TileMoving TileMoving => tileMoving;

    [SerializeField] private TileDragging tileDragging;

    public TileDragging TileDragging => tileDragging;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadTileProperties();
        LoadTileMoving();
        LoadTileDragging();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        tileProperties.Sprite.enabled = true;
    }

    private void LoadModel()
    {
        if (model != null) return;

        model = transform.GetChild(0);

        model.gameObject.SetActive(false);
    }

    private void LoadTileProperties()
    {
        if (tileProperties != null) return;

        tileProperties = GetComponentInChildren<TileProperties>();
    }

    private void LoadTileMoving()
    {
        if (tileMoving != null) return;

        tileMoving = GetComponentInChildren<TileMoving>();
    }

    private void LoadTileDragging()
    {
        if (tileDragging != null) return;

        tileDragging = GetComponentInChildren<TileDragging>();
    }

    public void OnDespawnAnim()
    {
        tileProperties.Sprite.enabled = false;
        model.gameObject.SetActive(true);
    }

    public void OffDespawnAnim()
    {
        model.gameObject.SetActive(false);
    }
}