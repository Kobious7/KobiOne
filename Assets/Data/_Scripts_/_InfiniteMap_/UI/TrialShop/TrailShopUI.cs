using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrailShopUI : GMono
{
    [SerializeField] private Button transBG, closeBtn;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (transBG == null) transBG = transform.Find("TransBG").GetComponent<Button>();
        if (closeBtn == null) closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        transBG.onClick.AddListener(CloseClickListener);
        closeBtn.onClick.AddListener(CloseClickListener);

        this.gameObject.SetActive(false);
    }

    private void CloseClickListener()
    {
        this.gameObject.SetActive(false);

        this.transform.parent.GetComponent<Button>().interactable = true;

        InfiniteMapManager.Instance.IsUIOpening = false;
    }
}