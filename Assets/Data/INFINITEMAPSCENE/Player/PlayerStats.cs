using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace InfiniteMap
{
    public class PlayerStats : PlayerAb
    {
        [SerializeField] private int level = 1;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        [SerializeField] private int requiredExp;

        public int RequiredExp
        {
            get { return requiredExp; }
            set { requiredExp = value; }
        }

        [SerializeField] private int currentExp;

        public int CurrentExp
        {
            get { return currentExp; }
            set { currentExp = value; }
        }

        [SerializeField] private int remainPoints;

        public int RemainPoints
        {
            get { return remainPoints; }
            set { remainPoints = value; }
        }

        //=======Start Potential======

        [SerializeField] private int power;

        public int Power
        {
            get { return power; }
            set { power = value; }
        }

        [SerializeField] private int magic;

        public int Magic
        {
            get { return magic; }
            set { magic = value; }
        }

        [SerializeField] private int strength;

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        [SerializeField] private int defense;

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        [SerializeField] private int dexterity;

        public int Dexterity
        {
            get { return dexterity;}
            set { dexterity = value;}
        }

        //=======End Potential======

        //=======Start Stats======

        [SerializeField] private int maxHP;

        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        [SerializeField] private int swordrainDamage;

        public int SwordrainDamage
        {
            get { return swordrainDamage; }
            set { swordrainDamage = value; }
        }

        [SerializeField] private int slashDamage;

        public int SlashDamage
        {
            get { return slashDamage; }
            set { slashDamage = value; }
        }

        //=======End Stats======

        protected override void Start()
        {
            base.Start();

            if(Game.Instance.MapData.MapCanLoad)
            {
                level = Game.Instance.MapData.PlayerInfo.Level;
                currentExp = Game.Instance.MapData.PlayerInfo.CurrentExp;
                requiredExp = Game.Instance.MapData.PlayerInfo.Level * 100 + (Game.Instance.MapData.PlayerInfo.Level / 10) * 1000;
            }
            else
            {
                level = 1;
                currentExp = 0;
                requiredExp = 100;
            }
            ExpCheck();
        }

        private void ExpCheck()
        {
            if(Game.Instance.MapData.MapCanLoad)
            {
                if(Game.Instance.MapData.Result == Result.WIN)
                {
                    int exp = Random.Range(Game.Instance.MapData.MonsterInfo.Level * 50, Game.Instance.MapData.MonsterInfo.Level * 70);

                    Debug.Log("" + exp);

                    currentExp += exp;

                    while(currentExp >= requiredExp)
                    {
                        level++;
                        currentExp -= requiredExp;
                        requiredExp = level * 100 + (level / 10) * 1000;
                    }
                }
            }
        }
    }
}