using UnityEngine;

public class DestructiveObjectSpawner : Spawner
{
    private static DestructiveObjectSpawner instance;

    public static DestructiveObjectSpawner Instance => instance;

    [SerializeField] private Transform destructiveObject;

    public Transform DestructiveObject => destructiveObject;

    [SerializeField] private int spawnedCount;

        public int SpawnedCount
        {
            get => spawnedCount;
            set => spawnedCount = value;
        }

    [SerializeField] private SkillSO qSkill;
    [SerializeField] private SkillSO eSkill;
    [SerializeField] private SkillSO spaceSkill;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 DestructiveObjectSpawner is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        LoadSkillSOs();
        LoadDestructiveObject();
        LoadPrefabsResources();
        base.LoadComponents();
    }

    private void LoadSkillSOs()
    {
        qSkill = FindObjectOfType<Skills>().QSkill;
    }

    private void LoadDestructiveObject()
    {
        if(destructiveObject != null) return;

        destructiveObject = transform.Find("DestructiveObject");
    }

    private void LoadPrefabsResources()
    {
        Transform prefabContainer = transform.Find("Prefabs");

        if(qSkill != null && qSkill is TileSkillSO tileSkillSO)
        {
            Transform prefab = Instantiate(destructiveObject);
            prefab.name = "Q";

            prefab.SetParent(prefabContainer);
            prefab.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            DestructiveObject obj = prefab.GetComponent<DestructiveObject>();
            obj.SetSprite(tileSkillSO.ObjectSprite);
            obj.AdjustCollider();
        }

        if(qSkill != null && qSkill is OpSkillSO opSkillSO)
        {
            Transform prefab = Instantiate(destructiveObject);
            prefab.name = "Q";

            prefab.SetParent(prefabContainer);
            prefab.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            DestructiveObject obj = prefab.GetComponent<DestructiveObject>();
            obj.SetSprite(opSkillSO.ObjectSprite);
            obj.AdjustCollider();
        }

        destructiveObject.gameObject.SetActive(false);
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