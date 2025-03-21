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
            PlayerInfo playerInfo = Game.Instance.MapData.PlayerInfo;

            if (!Game.Instance.MapData.MapCanLoad) return;

            Stats.Attack = playerInfo.Attack;
            Stats.MagicAttack = playerInfo.MagicAttack;
            Stats.MaxHP = playerInfo.HP;
            Stats.CurrentHP = Stats.MaxHP;
            Stats.SwordrainDamage = playerInfo.SwordrainDamage;
            Stats.SlashDamage = playerInfo.SlashDamage;
            Stats.Defense = playerInfo.Defense;
            Stats.Accuracy = playerInfo.Accuracy;
            Stats.DamageRange = playerInfo.DamageRange;
            Stats.CritRate = playerInfo.CritRate;
            Stats.CritDamage = playerInfo.CritDamage;
        }

        protected override void LoadModel()
        {
            base.LoadModel();

            PlayerStatic.Instance.transform.Find("RigModel").transform.SetParent(Model);
            Model.Find("RigModel").transform.position = Model.parent.localPosition;
        }


    }
}