using UnityEngine;

public class SkillTreeUI : GMono
{
    [SerializeField] private SkillNodeUI t1b0;
    [SerializeField] private SkillNodeUI t2b1;
    [SerializeField] private SkillNodeUI t2b2;
    [SerializeField] private SkillNodeUI t2b3;
    [SerializeField] private SkillNodeUI t3b1;
    [SerializeField] private SkillNodeUI t3b2;
    [SerializeField] private SkillNodeUI t3b3;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTree();
    }

    private void LoadSkillTree()
    {
        if(t1b0 == null)
            t1b0 = transform.Find("T1B0").GetComponent<SkillNodeUI>();
        if(t2b1 == null)
            t2b1 = transform.Find("T2B1").GetComponent<SkillNodeUI>();
        if(t2b2 == null)
            t2b2 = transform.Find("T2B2").GetComponent<SkillNodeUI>();
        if(t2b3 == null)
            t2b3 = transform.Find("T2B3").GetComponent<SkillNodeUI>();
        if(t3b1 == null)
            t3b1 = transform.Find("T3B1").GetComponent<SkillNodeUI>();
        if(t3b2 == null)
            t3b2 = transform.Find("T3B2").GetComponent<SkillNodeUI>();
        if(t3b3 == null)
            t3b3 = transform.Find("T3B3").GetComponent<SkillNodeUI>();
    }

    public void ShowSkillTree(SkillTree skillTree, int treeIndex)
    {
        t1b0.ShowSkill(skillTree.T1B0, treeIndex);
        t2b1.ShowSkill(skillTree.T2B1, treeIndex);
        t2b2.ShowSkill(skillTree.T2B2, treeIndex);
        t2b3.ShowSkill(skillTree.T2B3, treeIndex);
        t3b1.ShowSkill(skillTree.T3B1, treeIndex);
        t3b2.ShowSkill(skillTree.T3B2, treeIndex);
        t3b3.ShowSkill(skillTree.T3B3, treeIndex);
    }
}