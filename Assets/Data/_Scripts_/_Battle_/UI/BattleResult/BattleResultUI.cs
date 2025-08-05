using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleResultUI : GMono
{
    [SerializeField] private Image win, lose, opacityBG;
    [SerializeField] private TextMeshProUGUI hint;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (win == null) win = transform.Find("Win").GetComponent<Image>();
        if (lose == null) lose = transform.Find("Lose").GetComponent<Image>();
        if (opacityBG == null) opacityBG = transform.Find("TransBG").GetComponent<Image>();
        if (hint == null) hint = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        Battle.Instance.OnPlayerLost += LostResult;
        Battle.Instance.OnMonsterLost += WinResult;

        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        opacityBG.gameObject.SetActive(false);
        hint.gameObject.SetActive(false);
    }

    private void WinResult()
    {
        win.gameObject.SetActive(true);
        opacityBG.gameObject.SetActive(true);
        hint.gameObject.SetActive(true);
    }

    private void LostResult()
    {
        lose.gameObject.SetActive(true);
        opacityBG.gameObject.SetActive(true);
        hint.gameObject.SetActive(true);
    }
}