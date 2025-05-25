using UnityEngine;

public class DestructiveObjectSpawner : Spawner
{
    private static DestructiveObjectSpawner instance;

    public static DestructiveObjectSpawner Instance => instance;

    [SerializeField] private Transform destructiveObject;

    public Transform DestructiveObject => destructiveObject;

    [SerializeField] private int tileSpawnCount;

    public int TileSpawnCount
    {
        get => tileSpawnCount;
        set => tileSpawnCount = value;
    }

    [SerializeField] private int opSpawnCount;

    public int OpSpawnCount
    {
        get => opSpawnCount;
        set => opSpawnCount = value;
    }

    [SerializeField] private SkillNode qSkill;
    [SerializeField] private SkillNode eSkill;
    [SerializeField] private SkillNode spaceSkill;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 DestructiveObjectSpawner is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    private void LoadSkillSOs()
    {
        qSkill = SkillB.Instance.QSkill;
        eSkill = SkillB.Instance.ESkill;
        spaceSkill = SkillB.Instance.SpaceSkill;
    }

    private void LoadDestructiveObject()
    {
        if(destructiveObject != null) return;

        destructiveObject = transform.Find("DestructiveObject");
    }

    public void LoadPrefabsResources()
    {
        LoadSkillSOs();
        LoadDestructiveObject();

        Transform prefabContainer = transform.Find("Prefabs");

        CreatePrefab(qSkill, prefabContainer, "Q");
        CreatePrefab(eSkill, prefabContainer, "E");
        CreatePrefab(spaceSkill, prefabContainer, "Space");
        destructiveObject.gameObject.SetActive(false);
        LoadPrefabs();
    }

    private void CreatePrefab(SkillNode skill, Transform prefabContainer, string prefabName)
    {
        if(skill != null && skill.skillSO is TileSkillSO tileSkillSO)
        {
            Transform prefab = Instantiate(destructiveObject);
            prefab.name = prefabName;

            prefab.SetParent(prefabContainer);
            prefab.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            DestructiveObject obj = prefab.GetComponent<DestructiveObject>();
            obj.SetSprite(tileSkillSO.ObjectSprite);
            obj.AdjustCollider();
        }

        if(skill != null && skill.skillSO is OpSkillSO opSkillSO && opSkillSO.ObjectMaxSpawnCount > 0)
        {
            Transform prefab = Instantiate(destructiveObject);
            prefab.name = prefabName;

            prefab.SetParent(prefabContainer);
            prefab.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            DestructiveObject obj = prefab.GetComponent<DestructiveObject>();
            obj.SetSprite(opSkillSO.ObjectSprite);
            obj.AdjustCollider();
        }
    }

    public Transform GetPrefabsByName(string name)
    {
        foreach(var prefab in prefabs)
        {
            if(prefab.name == name) return prefab;
        }

        return null;
    }
}