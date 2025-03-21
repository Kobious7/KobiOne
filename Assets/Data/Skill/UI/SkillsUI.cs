using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class SkillsUI : GMono
    {
        [SerializeField] private Button q;

        public Button Q => q;

        private DestructiveObjectSpawner destructiveObjectSpawner;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadQButton();
        }

        protected override void Start()
        {
            base.Start();
            destructiveObjectSpawner = DestructiveObjectSpawner.Instance;
            q.onClick.AddListener(QClick);
        }

        private void LoadQButton()
        {
            if(q != null) return;

            q = transform.Find("Q").GetComponent<Button>();
        }

        private void QClick()
        {
            if(SkillB.Instance.QSkill.skillSO is TileSkillSO) StartCoroutine(SpawnDestructiveObject());
        }

        private IEnumerator SpawnDestructiveObject()
        {
            TileSkillSO tileSkill = SkillB.Instance.QSkill.skillSO as TileSkillSO;

            //SkillB.Instance.Q.QTile.TargetsFinder.GetTileTargets();
            Game.Instance.Player.Stats.ManaDes(tileSkill.ManaCost);

            // destructiveObjectSpawner.SpawnedCount = tileSkill.ObjectSpawnCount;

            // for(int i = 0; i < tileSkill.ObjectSpawnCount; i++)
            // {
            //     Transform newObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), destructiveObjectSpawner.transform.position, Quaternion.identity);
            //     newObj.GetComponent<DestructiveObject>().Target = SkillB.Instance.Q.QTile.TargetsFinder.TileTargets[i];

            //     newObj.gameObject.SetActive(true);
            // }

            // if(tileSkill.AnotherTargets == SkillTarget.NONE)
            // {
            //     Transform newObj = destructiveObjectSpawner.Spawn(destructiveObjectSpawner.GetPrefabsByName("Q"), destructiveObjectSpawner.transform.position, Quaternion.identity);
            //     newObj.GetComponent<DestructiveObject>().Target = Skills.Instance.Q.QTile.Opponent;

            //     newObj.gameObject.SetActive(true);
            // }


                yield return null;


            Battle.Instance.TurnCount--;

            StartCoroutine(Game.Instance.Board.BoardDestroyedMatches.SkillDestroyAndFill());
        }
    }
}