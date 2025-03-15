using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Battle
{
    public class SkillUIClick : SkillUIAb
    {
        public Transform slot;
        public SkillButton skillButton;

        protected override void Start()
        {
            Skill.Btn.onClick.AddListener(Click);
        }

        private void Update()
        {
            Press();
        }

        private void Press()
        {
            if (InputManager.Instance.QPressed && skillButton == SkillButton.Q
                || InputManager.Instance.EPressed && skillButton == SkillButton.E
                || InputManager.Instance.SpacePressed && skillButton == SkillButton.Space)
            {
                if (Skill.SkillUILock.IsLock) return;

                StartCoroutine(SpawnSkill());
                InputManager.Instance.QPressed = false;
                InputManager.Instance.EPressed = false;
                InputManager.Instance.SpacePressed = false;
            }
        }

        private void Click()
        {
            StartCoroutine(SpawnSkill());
        }

        private IEnumerator SpawnSkill()
        {
            Skill skill;

            if (skillButton == SkillButton.Q) skill = Game.Instance.PlayerSkill[0];
            else if (skillButton == SkillButton.E) skill = Game.Instance.PlayerSkill[1];
            else skill = Game.Instance.PlayerSkill[2];

            Game.Instance.Player.Stats.ManaDes(skill.Properties.Consume);
            Game.Instance.TileSpawner.GetGeneratedTilesList();

            Game.Instance.TileSpawner.MarkList = new();
            Game.Instance.SkillSpawner.SpawnedCount = skill.Properties.SpawnCount;

            skill.FindTarget.GetTargets();

            for (int i = 0; i < skill.Properties.SpawnCount; i++)
            {
                Transform obj = Game.Instance.SkillSpawner.Spawn(slot, Game.Instance.SkillSpawner.transform.position, Quaternion.identity);

                obj.gameObject.SetActive(true);
                obj.GetComponent<FlyingObj>().Target = skill.FindTarget.Targets[i];
            }

            while (Game.Instance.SkillSpawner.SpawnedCount > 0)
            {
                yield return null;
            }

            Battle.Instance.TurnCount--;

            StartCoroutine(Game.Instance.Board.BoardDestroyedMatches.SkillDestroyAndFill());
        }
    }
}