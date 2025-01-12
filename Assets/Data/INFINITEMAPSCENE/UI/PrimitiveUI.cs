using UnityEngine;

namespace InfiniteMap
{
    public class PrimitiveUI : GMono
{
    [SerializeField] private bool closed;

    public bool Closed
    {
        get => closed;
        set => closed = value;
    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
        closed = false;
    }

    public void Open()
    {
        transform.gameObject.SetActive(true);
        closed = true;
    }
}
}