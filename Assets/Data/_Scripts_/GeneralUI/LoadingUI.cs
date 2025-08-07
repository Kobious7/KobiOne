using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : GMono
{
    [SerializeField] private Image image;
    [SerializeField] private int waitFrame;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (image == null) image = GetComponent<Image>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitFrames(waitFrame));
    }

    private IEnumerator WaitFrames(int frame)
    {
        for (int i = 0; i < frame; i++)
        {
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}