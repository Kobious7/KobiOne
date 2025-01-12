using UnityEngine;
using UnityEngine.UI;
using InfiniteMap;
using System.Collections.Generic;
using System.Linq;

public class SkillUISpawner : Spawner
{
    private static SkillUISpawner instance;

    public static SkillUISpawner Instance => instance;

    [SerializeField] private List<SkillSO> ownedList;
    [SerializeField] private List<SkillUI> skillList;

    public List<SkillUI> SkillList
    {
        get => skillList;
        set => skillList = value;
    }

    [SerializeField] private Transform currentActive;

    public Transform CurrentActive
    {
        get => currentActive;
        set => currentActive = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.LogError("Only 1 SkillUISpawner is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        SpawnSkillUI();
        LoadSkillList();
    }

    protected override void LoadHolder()
    {
        if(holder != null) return;

        holder = GetComponent<ScrollRect>().content;
    }

    public void SpawnSkillUI()
    {
        for(int i = 0; i < ownedList.Count; i++)
        {
            Transform newItem = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            newItem.transform.localScale = Vector3.one;
            
            SkillUI skillUI = newItem.GetComponent<SkillUI>();
            skillUI.SkillIndex = i;
            skillUI.SkillSO = ownedList[i];

            skillUI.ShowSkillUI();
            newItem.gameObject.SetActive(true);
        }
    }

    private void LoadSkillList()
    {
        skillList = GetComponentsInChildren<SkillUI>().ToList();
    }

    public SkillUI GetSkillUI(int index)
    {
        return skillList[index];
    }
}