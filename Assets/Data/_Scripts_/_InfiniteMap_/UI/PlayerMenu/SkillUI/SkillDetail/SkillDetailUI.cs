using UnityEngine;

public class SkillDetailUI : GMono
{
    private static SkillDetailUI instance;

    public static SkillDetailUI Instance => instance;

    [SerializeField] private SkillDetailSVUI detailUI;

    public SkillDetailSVUI DetailUI => detailUI;

    [SerializeField] private SkillDetailUIActionBtns actionBtns;

    public SkillDetailUIActionBtns ActionBtns => actionBtns;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 SkillDetailUI is allowed to exist!");

        instance = this;
        
        transform.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadDetailUI();
        LoadActionBtns();
    }

    protected override void Start()
    {
        base.Start();

        actionBtns.OnSkillUpgrading += ShowAgain;
    }

    private void LoadDetailUI()
    {
        if(detailUI != null) return;

        detailUI = GetComponentInChildren<SkillDetailSVUI>();
    }

    private void LoadActionBtns()
    {
        if(actionBtns != null) return;

        actionBtns = GetComponentInChildren<SkillDetailUIActionBtns>();
    }

    public void ShowSkilDetailsUI(SkillNode skill, int treeIndex, bool treeActive)
    {
        transform.gameObject.SetActive(true);
        detailUI.ShowText(skill);
        actionBtns.AddClickListeners(skill, treeIndex, treeActive);
    }

    private void ShowAgain(SkillNode skill, int treeIndex, bool treeActive)
    {
        detailUI.ShowText(skill);
        actionBtns.AddClickListeners(skill, treeIndex, treeActive);
    }
}