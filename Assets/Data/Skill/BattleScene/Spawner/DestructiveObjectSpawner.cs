using UnityEngine;

namespace Battle
{
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
            qSkill = FindObjectOfType<SkillB>().QSkill;
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

            if(qSkill != null && qSkill.skillSO is TileSkillSO tileSkillSO)
            {
                Transform prefab = Instantiate(destructiveObject);
                prefab.name = "Q";

                prefab.SetParent(prefabContainer);
                prefab.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

                DestructiveObject obj = prefab.GetComponent<DestructiveObject>();
                obj.SetSprite(tileSkillSO.ObjectSprite);
                obj.AdjustCollider();
            }

            if(qSkill != null && qSkill.skillSO is OpSkillSO opSkillSO)
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
            LoadPrefabs();
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
}