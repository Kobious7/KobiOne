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
    [SerializeField] private Transform hint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSkillTree();

        hint = transform.Find("Hint");
    }

    private void LoadSkillTree()
    {
        if (root == null)
            root = transform.Find("Root").GetComponent<SkillNodeUI>();
        if (attack1 == null)
            attack1 = transform.Find("Attack1").GetComponent<SkillNodeUI>();
        if (attack2 == null)
            attack2 = transform.Find("Attack2").GetComponent<SkillNodeUI>();
        if (attack3 == null)
            attack3 = transform.Find("Attack3").GetComponent<SkillNodeUI>();
        if (support1 == null)
            support1 = transform.Find("Support1").GetComponent<SkillNodeUI>();
        if (support2 == null)
            support2 = transform.Find("Support2").GetComponent<SkillNodeUI>();
        if (support3 == null)
            support3 = transform.Find("Support3").GetComponent<SkillNodeUI>();
    }

    public void ShowSkillTree(SkillTree skillTree, int treeIndex)
    {
        CheckSkillTreeCompatible(skillTree);
        root.ShowSkill(skillTree.Root, treeIndex, skillTree.IsActive);
        attack1.ShowSkill(skillTree.Attack1, treeIndex, skillTree.IsActive);
        attack2.ShowSkill(skillTree.Attack2, treeIndex, skillTree.IsActive);
        attack3.ShowSkill(skillTree.Attack3, treeIndex, skillTree.IsActive);
        support1.ShowSkill(skillTree.Support1, treeIndex, skillTree.IsActive);
        support2.ShowSkill(skillTree.Support2, treeIndex, skillTree.IsActive);
        support3.ShowSkill(skillTree.Support3, treeIndex, skillTree.IsActive);
    }

    public void ResetUpgradeButton(SkillTree skillTree, int treeIndex)
    {
        root.UpdateClick(skillTree.Root, treeIndex, skillTree.IsActive);
        attack1.UpdateClick(skillTree.Attack1, treeIndex, skillTree.IsActive);
        attack2.UpdateClick(skillTree.Attack2, treeIndex, skillTree.IsActive);
        attack3.UpdateClick(skillTree.Attack3, treeIndex, skillTree.IsActive);
        support1.UpdateClick(skillTree.Support1, treeIndex, skillTree.IsActive);
        support2.UpdateClick(skillTree.Support2, treeIndex, skillTree.IsActive);
        support3.UpdateClick(skillTree.Support3, treeIndex, skillTree.IsActive);
    }

    public void CheckSkillTreeCompatible(SkillTree skillTree)
    {
        if (skillTree.IsActive)
        {
            hint.gameObject.SetActive(false);
        }
        else
        {
            hint.gameObject.SetActive(true);
        }
    }
}