using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle
{
    public class Player : Entity
    {
        protected override void Start()
        {
            base.Start();
            if (!Game.Instance.MapData.MapCanLoad) return;

            Stats.MaxHP = Game.Instance.MapData.PlayerInfo.HP;
            Stats.SwordrainDamage = Game.Instance.MapData.PlayerInfo.SwordrainDamage;
            Stats.SlashDamage = Game.Instance.MapData.PlayerInfo.SlashDamage;
            Stats.CurrentHP = Stats.MaxHP;
        }

        protected override void LoadModel()
        {
            base.LoadModel();

            PlayerStatic.Instance.transform.Find("RigModel").transform.SetParent(Model);
            Model.Find("RigModel").transform.position = Model.parent.localPosition;
        }


    }
}