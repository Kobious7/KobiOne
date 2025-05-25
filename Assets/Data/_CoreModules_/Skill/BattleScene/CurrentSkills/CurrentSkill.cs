using UnityEngine;

public class CurrentSkill : SkillBAb
{
    [SerializeField] protected TileSkill tile;

    public TileSkill Tile => tile;

    [SerializeField] private SelfSkill self;

    public SelfSkill Self => self;

    [SerializeField] private OpSkill op;

    public OpSkill Op => op;

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
        
        LoadType();
    }

    private void LoadQTile()
    {
        if(tile != null) return;

        tile = GetComponentInChildren<TileSkill>();
    }

    private void LoadQSelf()
    {
        if (self != null) return;

        self = GetComponentInChildren<SelfSkill>();
    }

    private void LoadQOp()
    {
        if(op != null) return;

        op = GetComponentInChildren<OpSkill>();
    }

    private void LoadType()
    {
        SkillSO skillSO = GetSkillSO();

        if(skillSO == null)
        {
            tile.gameObject.SetActive(false); 
            self.gameObject.SetActive(false);
            op.gameObject.SetActive(false);
        }
        else
        {
            if(skillSO is TileSkillSO tileSkill)
            {
                tile.gameObject.SetActive(true);
                tile.TargetsFinder.SkillProps = tileSkill;
                self.gameObject.SetActive(false);
                op.gameObject.SetActive(false);
            }

            if(skillSO is SelfSkillSO)
            {
                tile.gameObject.SetActive(false); 
                self.gameObject.SetActive(true);
                op.gameObject.SetActive(false);
            }

            if(skillSO is OpSkillSO)
            {
                tile.gameObject.SetActive(false); 
                self.gameObject.SetActive(false);
                op.gameObject.SetActive(true);
            }
        }
    }

    protected virtual SkillSO GetSkillSO()
    {
        return null;
    }
}