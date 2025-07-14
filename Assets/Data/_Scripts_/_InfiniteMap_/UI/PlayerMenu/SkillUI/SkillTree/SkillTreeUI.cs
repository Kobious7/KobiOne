using UnityEngine;

public class SkillTreeUI : GMono
{
    [SerializeField] private SkillNodeUI root;
    [SerializeField] private SkillNodeUI attack1;
    [SerializeField] private SkillNodeUI attack2;
    [SerializeField] private SkillNodeUI attack3;
    [SerializeField] private SkillNodeUI support1;
    [SerializeField] private SkillNodeUI support2;
    [SerializeField] private SkillNodeUI support3;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTree();
    }

    private void LoadSkillTree()
    {
        if(root == null)
            root = transform.Find("Root").GetComponent<SkillNodeUI>();
        if(attack1 == null)
            attack1 = transform.Find("Attack1").GetComponent<SkillNodeUI>();
        if(attack2 == null)
            attack2 = transform.Find("Attack2").GetComponent<SkillNodeUI>();
        if(attack3 == null)
            attack3 = transform.Find("Attack3").GetComponent<SkillNodeUI>();
        if(support1 == null)
            support1 = transform.Find("Support1").GetComponent<SkillNodeUI>();
        if(support2 == null)
            support2 = transform.Find("Support2").GetComponent<SkillNodeUI>();
        if(support3 == null)
            support3 = transform.Find("Support3").GetComponent<SkillNodeUI>();
    }

    public void ShowSkillTree(SkillTree skillTree, int treeIndex)
    {
        root.ShowSkill(skillTree.Root, treeIndex);
        attack1.ShowSkill(skillTree.Attack1, treeIndex);
        attack2.ShowSkill(skillTree.Attack2, treeIndex);
        attack3.ShowSkill(skillTree.Attack3, treeIndex);
        support1.ShowSkill(skillTree.Support1, treeIndex);
        support2.ShowSkill(skillTree.Support2, treeIndex);
        support3.ShowSkill(skillTree.Support3, treeIndex);
    }
}