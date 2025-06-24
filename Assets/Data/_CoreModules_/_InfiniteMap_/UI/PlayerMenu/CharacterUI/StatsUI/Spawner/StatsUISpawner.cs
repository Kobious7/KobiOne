using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatsUISpawner : Spawner
{

    private static StatsUISpawner instance;

    public static StatsUISpawner Instance => instance;

    [SerializeField] private List<StatUI> stats;

    [SerializeField] private List<LineStatUI> lineStatUIs;
    private List<Stat> playerStats;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 StatUISpawner is allowed to exist!");

        instance = this;
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;
        
        playerStats = InfiniteMapManager.Instance.Player.StatsSystem.Stats;

        SpawnStatLine();
        GetStatUIList();
        InfiniteMapManager.Instance.Player.StatsSystem.OnStatChange += UpdateStat;
    }

    private void SpawnStatLine()
    {
        foreach(var stat in playerStats)
        {
            Transform newStat = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newStat.transform.localScale = Vector3.one;
            LineStatUI line = newStat.GetComponent<LineStatUI>();

            line.ShowData(stat);
            newStat.gameObject.SetActive(true);
        }
    }

    public void GetStatUIList()
    {
        lineStatUIs = holder.GetComponentsInChildren<LineStatUI>().ToList();
    }

    public LineStatUI GetLineStatUI(int index)
    {
        return lineStatUIs[index];
    }

    private void UpdateStat(int index)
    {
        LineStatUI statUI = GetLineStatUI(index);

        statUI.ShowData(playerStats[index]);
    }
}