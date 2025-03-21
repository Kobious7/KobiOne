using UnityEngine;
using Battle;

public class Q : SkillBAb
{
    [SerializeField] private QTile qTile;

    public QTile QTile => qTile;

    [SerializeField] private QSelf qSelf;

    public QSelf QSelf => qSelf;

    [SerializeField] private QOp qOp;

    public QOp QOp => qOp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadQTile();
        LoadQSelf();
        LoadQOp();
    }

    protected override void Start()
    {
        base.Start();
        
        LoadQType();
    }

    private void LoadQTile()
    {
        if(qTile != null) return;

        qTile = GetComponentInChildren<QTile>();
    }

    private void LoadQSelf()
    {
        if (qSelf != null) return;

        qSelf = GetComponentInChildren<QSelf>();
    }

    private void LoadQOp()
    {
        if(qOp != null) return;

        qOp = GetComponentInChildren<QOp>();
    }

    private void LoadQType()
    {
        if(SkillB.QSkill.skillSO is TileSkillSO)
        {
            qTile.gameObject.SetActive(true); 
            qSelf.gameObject.SetActive(false);
            qOp.gameObject.SetActive(false);
        }

        if(SkillB.QSkill.skillSO is SelfSkillSO)
        {
            qTile.gameObject.SetActive(false); 
            qSelf.gameObject.SetActive(true);
            qOp.gameObject.SetActive(false);
        }

        if(SkillB.QSkill.skillSO is OpSkillSO)
        {
            qTile.gameObject.SetActive(false); 
            qSelf.gameObject.SetActive(false);
            qOp.gameObject.SetActive(true);
        }
    }
}