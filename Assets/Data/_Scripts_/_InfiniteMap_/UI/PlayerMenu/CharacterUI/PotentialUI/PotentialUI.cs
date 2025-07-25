using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotentialUI : GMono
{
    [SerializeField] private List<PotentialStatUI> potentialStatUIs;
    [SerializeField] private TextMeshProUGUI remainPoint;
    [SerializeField] private Button resetBtn;
    [SerializeField] private ResetPromptUI resetPromptUI;
    private List<Stat> potential;
    private InfiniteMapManager infiniteMapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPotentialStatUIs();
        LoadRemainPoint();

        resetBtn = transform.Find("ResetBtn").GetComponent<Button>();
        resetPromptUI = GetComponentInChildren<ResetPromptUI>();
    }

    protected override void Start()
    {
        base.Start();

        infiniteMapManager = InfiniteMapManager.Instance;

        StartCoroutine(WaitNextFrame());
    }

    private void LoadPotentialStatUIs()
    {
        if (potentialStatUIs.Count > 0) return;

        potentialStatUIs = GetComponentsInChildren<PotentialStatUI>().ToList();
    }

    private void LoadRemainPoint()
    {
        if (remainPoint != null) return;

        remainPoint = transform.Find("RemainPoints").Find("Point").GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        ShowPotentialPoint();
        infiniteMapManager.Player.StatsSystem.OnPotentialChange += UpdatePotential;

        resetBtn.onClick.RemoveAllListeners();
        resetBtn.onClick.AddListener(ClickToOpenPromptListener);
    }

    private void ShowPotentialPoint()
    {
        potential = infiniteMapManager.Player.StatsSystem.Potential;

        for (int i = 0; i < potentialStatUIs.Count; i++)
        {
            potentialStatUIs[i].ShowPoint(potential[i]);
            potentialStatUIs[i].AddClickListener(potential[i]);
        }

        remainPoint.text = $"{infiniteMapManager.Player.StatsSystem.RemainPoints}";
    }

    private void UpdatePotential(int index)
    {
        potentialStatUIs[index].ShowPoint(potential[index]);
        remainPoint.text = $"{infiniteMapManager.Player.StatsSystem.RemainPoints}";
    }
    
    private void ClickToOpenPromptListener()
    {
        resetPromptUI.gameObject.SetActive(true);
    }
}